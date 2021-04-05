module PoolTests

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
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Domain.Models
open PoolDataHelper
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
let ``given a pool with valid data when a request to add it is executed then a correct answer is gotten`` () =
    let poolsDomains = createPoolsToReturn ()

    let poolsDtos =
        poolsDomains |> Seq.map PoolViewMapper.mapToView

    let addPoolsAction = Substitute.For<IAddPoolsAction>()

    addPoolsAction
        .AsyncExecute(Arg.Any<PoolDomain seq>())
        .Returns(Task.FromResult(poolsDomains |> Array.toSeq)
                 |> Async.AwaitTask)
    |> ignore

    let builder =
        WebHostBuilder()
            .ConfigureTestServices(fun services ->
                services.AddScoped<Context>(fun provider -> createContext ())
                |> ignore

                services.AddScoped<QueryContext>(fun provider -> createQueryContext ())
                |> ignore

                services.AddTransient<IAddPoolsAction>(fun provider -> addPoolsAction)
                |> ignore)
            .UseEnvironment("Development")
            .UseStartup<Startup>()

    let server = new TestServer(builder)
    let client = server.CreateClient()

    let request =
        new HttpRequestMessage(HttpMethod("POST"), "/pool")

    let jsonOptions =
        JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

    let json =
        JsonSerializer.Serialize(poolsDtos, jsonOptions)

    request.Content <- new StringContent(json, Encoding.UTF8, "application/json")

    let response =
        client.SendAsync(request)
        |> Async.AwaitTask
        |> Async.RunSynchronously

    response.EnsureSuccessStatusCode() |> ignore
