module MatchTests

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
    
let createMatchesGroupsViewDaosToReturn () =
    [| { MatchGroupViewDao.GroupMatchId = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         MatchDate = DateTime.Now }
       { MatchGroupViewDao.GroupMatchId = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamName = "Argentina"
         AwayTeamName = "Perú"
         MatchDate = DateTime.Now } |]

[<Fact>]
let ``given a group match id when a request to query the matches by group match id then the matches are returned`` () =
    async {

        let matchesToReturn = createMatchesGroupsViewDaosToReturn ()

        let findMatchesByGroupMatchAction =
            Mock<IFindMatchesByGroupMatchAction>()

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