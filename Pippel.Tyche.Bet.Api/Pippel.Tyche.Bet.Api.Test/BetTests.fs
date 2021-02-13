module BetTests

open System
open System.Net.Http
open System.Text
open System.Text.Json
open System.Threading.Tasks
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.DependencyInjection
open Moq
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Api
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type
open Xunit

let createContext () =
    new Context(DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)

let createQueryContext () =
    new QueryContext(DbContextOptionsBuilder<QueryContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)

let createBetsToReturn () =
    [| { Bet.ID =
             "c6bc7971-1261-4bb8-9f57-b14013129315"
             |> Uuid.create
         GroupBetID =
             "718d7467-383f-4094-81c4-5b104d7969aa"
             |> Uuid.create
         GamblerID =
             "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
             |> Uuid.create
         MatchID =
             "107d3c40-faff-4980-8041-8763b4f43d42"
             |> Uuid.create
         HomeTeamValue = 0 |> PositiveInt.create
         AwayTeamValue = 1 |> PositiveInt.create }
       { Bet.ID =
             "67c53e8f-c4a6-4a1a-8b5b-55c902660395"
             |> Uuid.create
         GroupBetID =
             "43c2483f-9b3e-48b8-978c-99e07d2f29d2"
             |> Uuid.create
         GamblerID =
             "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
             |> Uuid.create
         MatchID =
             "107d3c40-faff-4980-8041-8763b4f43d42"
             |> Uuid.create
         HomeTeamValue = 2 |> PositiveInt.create
         AwayTeamValue = 1 |> PositiveInt.create } |]

let createEditingBetsDtosToSend () =
    [| { EditingBetDto.MatchID = Guid("718d7467-383f-4094-81c4-5b104d7969aa")
         GamblerID = Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
         GroupBetID = Guid("107d3c40-faff-4980-8041-8763b4f43d42")
         HomeTeamValue = 0
         AwayTeamValue = 1 }
       { EditingBetDto.MatchID = Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
         GamblerID = Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
         GroupBetID = Guid("107d3c40-faff-4980-8041-8763b4f43d42")
         HomeTeamValue = 2
         AwayTeamValue = 1 } |]

let createMatchesToReturn () =
    [| { MatchGamblerViewDao.GroupBetID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         GroupMatchID = Guid("697b3701-640e-4cef-a20a-12ce6e8d7e3b")
         GamblerID = Guid("718d7467-383f-4094-81c4-5b104d7969aa")
         GroupMatchName = "Eliminatorias al Mundial Qatar 2024 23/24 febrero"
         StartDate = DateTime.Now
         EndDate = DateTime.Now
         CurrentPoint = 10
         CurrentPosition = 3
         BeforePosition = 4 }
       { MatchGamblerViewDao.GroupBetID = Guid("67c53e8f-c4a6-4a1a-8b5b-55c902660395")
         GroupMatchID = Guid("f608cdd7-2694-4f46-b5ce-a2db030789da")
         GamblerID = Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
         GroupMatchName = "Eliminatorias al Mundial Qatar 2024 02/03 mayo"
         StartDate = DateTime.Now
         EndDate = DateTime.Now
         CurrentPoint = 15
         CurrentPosition = 8
         BeforePosition = 4 } |]
    
let createMatchesViewDaosToReturn () =
    [| { MatchViewDao.MatchID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         RoundMatchID = Guid("673e3cdf-77ed-4be8-80fa-5c3e4eed977a")
         GroupMatchID = Guid("f306c573-755c-45f7-bf7f-d715b572d51e")
         GroupBetID = Guid("19c6abc4-bc86-4b49-91c8-50c53dd8b3ad")
         MatchStatus = MatchStatus.Playing
         HomeTeamID = Guid("2144d3f2-ee6e-47ab-827c-6fc08df6af2a")
         AwayTeamID = Guid("e3a2551a-70ea-428f-bdb1-7a75f55f96da")
         MatchDate = DateTime.Now
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         Point = Nullable<int>(20) }
       { MatchViewDao.MatchID = Guid("efb920a9-6b50-46eb-8fb3-2044b9a7de55")
         RoundMatchID = Guid("d0dd2b75-cf37-4239-8dc3-a1179b250ab8")
         GroupMatchID = Guid("626a4dcf-b775-45aa-a82d-057de0cd4c36")
         GroupBetID = Guid("4fd172fd-b2f8-4518-b54b-bd50599ea672")
         MatchStatus = MatchStatus.Playing
         HomeTeamID = Guid("337824e9-c2bd-4680-b4ac-f5cac3c64f20")
         AwayTeamID = Guid("12a1cf96-c3f7-4ce3-9c22-0a8883889348")
         MatchDate = DateTime.Now
         HomeTeamName = "Argentina"
         AwayTeamName = "Uruguay"
         Point = Nullable<int>(10) } |]

[<Fact>]
let ``given several editing bets when a request to edit the bets is raised then a correct answer is gotten`` () =
    async {

        let betsToReturn = createBetsToReturn ()

        let editBetActionMock = Mock<IEditBetAction>()

        editBetActionMock
            .Setup(fun x -> x.AsyncExecute(It.IsAny<EditingBet seq>()))
            .Returns(Task.FromResult(betsToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore

        let builder =
            WebHostBuilder()
                .ConfigureTestServices(fun services ->
                    services.AddScoped<Context>(fun provider -> createContext ())
                    |> ignore

                    services.AddScoped<QueryContext>(fun provider -> createQueryContext ())
                    |> ignore

                    services.AddTransient<IEditBetAction>(fun provider -> editBetActionMock.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let editingBetsDtos = createEditingBetsDtosToSend ()

        let request =
            new HttpRequestMessage(HttpMethod("PUT"), "/bet/edit")

        let jsonOptions =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        let json =
            JsonSerializer.Serialize(editingBetsDtos, jsonOptions)

        request.Content <- new StringContent(json, Encoding.UTF8, "application/json")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a gambler id when a request to query the opened matches by gambler id then the opened matches are returned`` () =
    async {

        let matchesToReturn = createMatchesToReturn ()

        let findOpenedGroupsMatchesByGamblerAction =
            Mock<IFindOpenedGroupsMatchesByGamblerAction>()

        findOpenedGroupsMatchesByGamblerAction
            .Setup(fun x -> x.AsyncExecute(It.IsAny<Uuid>()))
            .Returns(Task.FromResult(matchesToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore

        let builder =
            WebHostBuilder()
                .ConfigureTestServices(fun services ->
                    services.AddScoped<Context>(fun provider -> createContext ())
                    |> ignore

                    services.AddScoped<QueryContext>(fun provider -> createQueryContext ())
                    |> ignore

                    services.AddTransient<IFindOpenedGroupsMatchesByGamblerAction>(fun provider ->
                        findOpenedGroupsMatchesByGamblerAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/bet/opened?gamblerid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a group bet id when a request to query the matches by group bet id then the matches are returned`` () =
    async {

        let matchesToReturn = createMatchesViewDaosToReturn ()

        let findMatchesByGroupBetAction =
            Mock<IFindMatchesByGroupBetAction>()

        findMatchesByGroupBetAction
            .Setup(fun x -> x.AsyncExecute(It.IsAny<Uuid>()))
            .Returns(Task.FromResult(matchesToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore

        let builder =
            WebHostBuilder()
                .ConfigureTestServices(fun services ->
                    services.AddScoped<Context>(fun provider -> createContext ())
                    |> ignore

                    services.AddScoped<QueryContext>(fun provider -> createQueryContext ())
                    |> ignore

                    services.AddTransient<IFindMatchesByGroupBetAction>(fun provider ->
                        findMatchesByGroupBetAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/bet/matches?groupbetid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }