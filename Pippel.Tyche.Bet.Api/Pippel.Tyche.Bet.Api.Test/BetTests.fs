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
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type
open BetDataHelper
open MatchDataHelper
open Xunit

let createContext () =
    new Context(DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)

let createQueryContext () =
    new QueryContext(DbContextOptionsBuilder<QueryContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)

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
    
[<Fact>]
let ``given a group bet id when a request to query the bet's positions by group bet id then the bet's positions are returned`` () =
    async {

        let betsPositionsToReturn = createBetsPositionsViewDaosToReturn ()

        let findBetsByGroupBetAction =
            Mock<IFindBetsByGroupBetAction>()

        findBetsByGroupBetAction
            .Setup(fun x -> x.AsyncExecute(It.IsAny<Uuid>()))
            .Returns(Task.FromResult(betsPositionsToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore

        let builder =
            WebHostBuilder()
                .ConfigureTestServices(fun services ->
                    services.AddScoped<Context>(fun provider -> createContext ())
                    |> ignore

                    services.AddScoped<QueryContext>(fun provider -> createQueryContext ())
                    |> ignore

                    services.AddTransient<IFindBetsByGroupBetAction>(fun provider ->
                        findBetsByGroupBetAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/bet/position?groupbetid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }
    
[<Fact>]
let ``given a group bet id when a request to get the on playing matches and the bet's positions by group bet id then the on playing matches and the bet's positions are returned`` () =
    async {

        let betsPositionsToReturn = createBetsPositionsViewDaosToReturn ()
        
        let matchesToReturn =
            createOnPlayingMatchesViewDaosToReturn ()

        let findBetsByGroupBetAction =
            Mock<IFindBetsByGroupBetAction>()

        findBetsByGroupBetAction
            .Setup(fun x -> x.AsyncExecute(It.IsAny<Uuid>()))
            .Returns(Task.FromResult(betsPositionsToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore
        
        let findOnPlayingMatchesByGroupMatchAction =
            Mock<IFindOnPlayingMatchesByGroupBetAction>()

        findOnPlayingMatchesByGroupMatchAction
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

                    services.AddTransient<IFindBetsByGroupBetAction>(fun provider ->
                        findBetsByGroupBetAction.Object)
                    |> ignore
                    
                    services.AddTransient<IFindOnPlayingMatchesByGroupBetAction>(fun provider ->
                        findOnPlayingMatchesByGroupMatchAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/bet/positionandonplayingmatches?groupbetid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

    