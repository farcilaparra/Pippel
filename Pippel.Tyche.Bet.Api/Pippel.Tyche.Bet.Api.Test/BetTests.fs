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
open NSubstitute
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Api
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type
open BetDataHelper
open MatchDataHelper
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid
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

        let editBetAction = Substitute.For<IEditBetAction>()

        editBetAction
            .AsyncExecute(Arg.Any<BetDomain seq>())
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

                    services.AddTransient<IEditBetAction>(fun provider -> editBetAction)
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

        jsonOptions.Converters.Add(UuidJsonConverter())
        jsonOptions.Converters.Add(PositiveIntJsonConverter())

        let json =
            JsonSerializer.Serialize(editingBetsDtos, jsonOptions)

        request.Content <- new StringContent(json, Encoding.UTF8, "application/json")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a gambler id when a request to query the opened master pools by gambler id then a correct answer is gotten`` () =
    async {

        let poolsReviewToReturn = createPoolsReviewToReturn ()

        let findOpenedMasterPoolsByGamblerAction =
            Substitute.For<IFindOpenedMasterPoolsByGamblerAction>()

        findOpenedMasterPoolsByGamblerAction
            .AsyncExecute(Arg.Any<Uuid>())
            .Returns(Task.FromResult(poolsReviewToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore

        let builder =
            WebHostBuilder()
                .ConfigureTestServices(fun services ->
                    services.AddScoped<Context>(fun provider -> createContext ())
                    |> ignore

                    services.AddScoped<QueryContext>(fun provider -> createQueryContext ())
                    |> ignore

                    services.AddTransient<IFindOpenedMasterPoolsByGamblerAction>(fun provider ->
                        findOpenedMasterPoolsByGamblerAction)
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
let ``given a pool id when a request to query the matches by pool id then a correct answer is gotten`` () =
    async {

        let matchesToReturn = createMatchesViewDaosToReturn ()

        let findMatchesByPoolAction =
            Substitute.For<IFindMatchesByPoolAction>()

        findMatchesByPoolAction
            .AsyncExecute(Arg.Any<Uuid>())
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

                    services.AddTransient<IFindMatchesByPoolAction>(fun provider -> findMatchesByPoolAction)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/bet/matches?poolid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a pool id when a request to query the bet's positions by pool id then a correct answer is gotten`` () =
    async {

        let betsPositionsToReturn = createBetsPositionsViewDaosToReturn ()

        let findBetsByPoolAction = Substitute.For<IFindBetsByPoolAction>()

        findBetsByPoolAction
            .AsyncExecute(Arg.Any<Uuid>())
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

                    services.AddTransient<IFindBetsByPoolAction>(fun provider -> findBetsByPoolAction)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"), "/bet/position?poolid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }

[<Fact>]
let ``given a pool id when a request to get the on playing matches and the bet's positions by pool id then a correct answer is gotten`` () =
    async {

        let betsPositionsToReturn = createBetsPositionsViewDaosToReturn ()

        let matchesToReturn =
            createOnPlayingMatchesViewDaosToReturn ()

        let findBetsByPoolAction = Substitute.For<IFindBetsByPoolAction>()

        findBetsByPoolAction
            .AsyncExecute(Arg.Any<Uuid>())
            .Returns(Task.FromResult(betsPositionsToReturn |> Array.toSeq)
                     |> Async.AwaitTask)
        |> ignore

        let findOnPlayingMatchesByPoolAction =
            Substitute.For<IFindOnPlayingMatchesByPoolAction>()

        findOnPlayingMatchesByPoolAction
            .AsyncExecute(Arg.Any<Uuid>())
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

                    services.AddTransient<IFindBetsByPoolAction>(fun provider -> findBetsByPoolAction)
                    |> ignore

                    services.AddTransient<IFindOnPlayingMatchesByPoolAction>(fun provider ->
                        findOnPlayingMatchesByPoolAction)
                    |> ignore)
                .UseEnvironment("Development")
                .UseStartup<Startup>()

        let server = new TestServer(builder)
        let client = server.CreateClient()

        let request =
            new HttpRequestMessage(HttpMethod("GET"),
                                   "/bet/positionandonplayingmatches?poolid=12d4f8c8-78e3-417f-ac0c-0fdb480d5b36")

        let! response = client.SendAsync(request) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore
    }
