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
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Type
open MatchDataHelper
open Xunit

let createContext () =
    new Context(
        DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(
            Guid.NewGuid().ToString()
        )
            .Options
    )

let createQueryContext () =
    new QueryContext(
        DbContextOptionsBuilder<QueryContext>()
            .UseInMemoryDatabase(
            Guid.NewGuid().ToString()
        )
            .Options
    )

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

                    services.AddTransient<IFindMatchesByGroupMatchAction>
                        (fun provider -> findMatchesByGroupMatchAction.Object)
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
let ``given a group bet id when a request to query the on playing matches by group bet id then the matches are returned``
    ()
    =
    async {

        let matchesToReturn =
            createOnPlayingMatchesViewDaosToReturn ()

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

                    services.AddTransient<IFindOnPlayingMatchesByGroupBetAction>
                        (fun provider -> findOnPlayingMatchesByGroupMatchAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(
                HttpMethod("GET"),
                "/match/onplaying?groupbetid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36"
            )

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a group bet id when a request to query the on playing matches and the bet's positions by group bet id then the on playing matches and bet's positions are returned``
    ()
    =
    async {

        let matchesToReturn =
            createOnPlayingMatchesViewDaosToReturn ()

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

                    services.AddTransient<IFindOnPlayingMatchesByGroupBetAction>
                        (fun provider -> findOnPlayingMatchesByGroupMatchAction.Object)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(
                HttpMethod("GET"),
                "/match/onplaying?groupbetid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36"
            )

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }
