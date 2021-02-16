namespace Pippel.Bet.Tyche.Api.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type BetController(logger: ILogger<BetController>,
                   matchGamblerViewMapper: MatchGroupGamblerViewMapper,
                   matchViewMapper: MatchViewMapper,
                   editingBetMapper: EditingBetMapper,
                   betPositionViewMapper: BetPositionViewMapper,
                   onPlayingMatchViewMapper: OnPlayingMatchViewMapper,
                   editBetAction: IEditBetAction,
                   findOpenedGroupsMatchesByGamblerAction: IFindOpenedGroupsMatchesByGamblerAction,
                   findMatchesByGroupBetAction: IFindMatchesByGroupBetAction,
                   findBetsByGroupBet: IFindBetsByGroupBetAction,
                   findOnPlayingMatchesByGroupBet: IFindOnPlayingMatchesByGroupBetAction) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.AsyncGetOpened(gamblerID: Guid): Async<MatchGroupGamblerViewDto seq> =
        async {
            let! items = findOpenedGroupsMatchesByGamblerAction.AsyncExecute(gamblerID |> Uuid.createFromGuid)

            return
                items
                |> Seq.map (fun x -> x |> matchGamblerViewMapper.Map)
        }

    [<HttpGet("matches")>]
    member this.AsyncGetMatches(groupBetID: Guid): Async<MatchViewDto seq> =
        async {
            let! items = findMatchesByGroupBetAction.AsyncExecute(groupBetID |> Uuid.createFromGuid)

            return
                items
                |> Seq.map (fun x -> x |> matchViewMapper.Map)
        }

    [<HttpPut("edit")>]
    member this.AsyncEditBet(editingBetsDtos: EditingBetDto []) =
        async {
            let! editedBets =
                editBetAction.AsyncExecute
                    (editingBetsDtos
                     |> Array.map (fun x -> x |> editingBetMapper.Map))

            return editedBets
        }

    [<HttpGet("position")>]
    member this.AsyncGetPositions(groupBetID: Guid): Async<BetPositionViewDto seq> =
        async {
            let! items = findBetsByGroupBet.AsyncExecute(groupBetID |> Uuid.createFromGuid)

            return
                items
                |> Seq.map (fun x -> x |> betPositionViewMapper.Map)
        }

    [<HttpGet("positionandonplayingmatches")>]
    member this.AsyncGetPositionsAndOnPlayingMatches(groupBetID: Guid): Async<BetPositionAndOnPlayingMatchViewDto> =
        async {
            let! positions = findBetsByGroupBet.AsyncExecute(groupBetID |> Uuid.createFromGuid)
            let! onPlayingMatches = findOnPlayingMatchesByGroupBet.AsyncExecute(groupBetID |> Uuid.createFromGuid)

            return
                { BetPositionAndOnPlayingMatchViewDto.BetsPositions =
                      (positions
                       |> Seq.map (fun x -> x |> betPositionViewMapper.Map))
                  OnPlayingMatches =
                      (onPlayingMatches
                       |> Seq.map (fun x -> x |> onPlayingMatchViewMapper.Map)) }
        }
