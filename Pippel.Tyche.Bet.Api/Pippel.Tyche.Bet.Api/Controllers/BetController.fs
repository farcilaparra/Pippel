namespace Pippel.Tax.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type BetController(logger: ILogger<BetController>,
                   matchGamblerViewMapper: MatchGamblerViewMapper,
                   matchViewMapper: MatchViewMapper,
                   editingBetMapper: EditingBetMapper,
                   editBetAction: IEditBetAction,
                   findOpenedGroupsMatchesByGamblerAction: IFindOpenedGroupsMatchesByGamblerAction,
                   findMatchesByGroupBetAction: IFindMatchesByGroupBetAction) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.AsyncGetOpened(gamblerID: Guid): Async<MatchGamblerViewDto seq> =
        async {
            let! items = findOpenedGroupsMatchesByGamblerAction.AsyncExecute(gamblerID |> Uuid.createFromGuid)

            return
                items
                |> Seq.map (fun x ->
                    x
                    |> matchGamblerViewMapper.MapToMatchGamblerViewDto)
        }

    [<HttpGet("matches")>]
    member this.AsyncGetMatches(groupBetID: Guid): Async<MatchViewDto seq> =
        async {
            let! items = findMatchesByGroupBetAction.AsyncExecute(groupBetID |> Uuid.createFromGuid)

            return
                items
                |> Seq.map (fun x -> x |> matchViewMapper.MapToMatchViewDto)
        }

    [<HttpPut("edit")>]
    member this.AsyncEditBet(editingBetsDtos: EditingBetDto []) =
        async {
            let! editedBets =
                editBetAction.AsyncExecute
                    (editingBetsDtos
                     |> Array.map (fun x -> x |> editingBetMapper.MapToEditingBet))

            return editedBets
        }
