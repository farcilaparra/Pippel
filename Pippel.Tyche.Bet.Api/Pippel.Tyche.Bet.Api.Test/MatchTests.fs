module MatchTests

open System
open System.Net.Http
open System.Threading.Tasks
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.DependencyInjection
open Moq
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Api
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries
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

let createMatchesGroupsViewDaosToReturn () =
    [| { MatchGroupViewDao.GroupMatchId = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         MatchDate = DateTime.Now }
       { MatchGroupViewDao.GroupMatchId = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamName = "Argentina"
         AwayTeamName = "Perú"
         MatchDate = DateTime.Now } |]

let createOnPlayingMatchesViewDaosToReturn () =
    [| { OnPlayingMatchViewDao.MatchID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamID = Guid("d557bea9-f5df-4115-b45a-ae240b8a19bc")
         AwayTeamID = Guid("7c63f1de-55d5-47d1-8831-4d2bdf3c0106")
         MatchDate = DateTime.Now
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         GroupMatchID = Guid("716cc6df-7ae0-4534-9d86-0e2438dfcfac") }
       { OnPlayingMatchViewDao.MatchID = Guid("044351a8-6bfa-4315-b7c7-874b45c95149")
         HomeTeamID = Guid("305e841b-cffc-4c62-9955-43d7021f0107")
         AwayTeamID = Guid("81488a27-5635-45e0-a223-4c76b1e1bcea")
         MatchDate = DateTime.Now
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         GroupMatchID = Guid("eee62b95-89a4-4d20-9b72-c83d7b6e5cba") } |]

[<Fact>]
let ``given a group match id when a request to query the matches by group match id then the matches are returned`` () =
    async {

        let matchesToReturn = createMatchesGroupsViewDaosToReturn ()

        let findMatchesByGroupMatchAction = Mock<IFindMatchesByGroupMatchAction>()

        findMatchesByGroupMatchAction
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

                    services.AddTransient<IFindMatchesByGroupMatchAction>(fun provider ->
                        findMatchesByGroupMatchAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/match?groupmatchid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a group bet id when a request to query the on playing matches by group bet id then the matches are returned`` () =
    async {

        let matchesToReturn =
            createOnPlayingMatchesViewDaosToReturn ()

        let findOnPlayingMatchesByGroupMatchAction =
            Mock<IFindOnPlayingMatchesByGroupMatchAction>()

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

                    services.AddTransient<IFindOnPlayingMatchesByGroupMatchAction>(fun provider ->
                        findOnPlayingMatchesByGroupMatchAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/match/onplaying?groupbetid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }
