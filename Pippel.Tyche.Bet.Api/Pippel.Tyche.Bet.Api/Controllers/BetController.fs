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
                   matchGamblerViewMapper: MatchGamblerViewMapper) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.GetOpened(gamblerID: string): Async<MatchGamblerViewDto seq> =
        async {
            let findOpenedGroupsMatchesAction =
                FindOpenedGroupsMatchesAction(queryRepositoryFactory.Get<MatchGamblerViewDao>())

            let! items = findOpenedGroupsMatchesAction.AsyncExecute(gamblerID |> Uuid.create)

            return
                items
                |> Seq.map (fun x -> x |> matchGamblerViewMapper.MapToSource)
        }
