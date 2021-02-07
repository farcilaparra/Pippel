namespace Pippel.Tax.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Actions
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type
open Pippel.Tyche.Bet.Data.Repositories.Queries

[<ApiController>]
[<Route("[controller]")>]
type BetController(logger: ILogger<BetController>,
                   queryRepositoryFactory: IQueryRepositoryFactory,
                   matchGamblerViewMapper: MatchGamblerViewMapper,
                   matchViewMapper: MatchViewMapper) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.GetOpened(gamblerID: string): Async<MatchGamblerViewDto seq> =
        async {
            let action =
                FindOpenedGroupsMatchesByGamblerAction(queryRepositoryFactory.Get<MatchGamblerViewDao>())

            let! items = action.AsyncExecute(gamblerID |> Uuid.create)

            return
                items
                |> Seq.map (fun x -> x |> matchGamblerViewMapper.MapToSource)
        }


    [<HttpGet("matches")>]
    member this.GetMatches(groupBetID: string): Async<MatchViewDto seq> =
        async {
            let action =
                FindMatchesByGroupBetAction(queryRepositoryFactory.Get<MatchViewDao>())

            let! items = action.AsyncExecute(groupBetID |> Uuid.create)

            return
                items
                |> Seq.map (fun x -> x |> matchViewMapper.MapToSource)
        }
