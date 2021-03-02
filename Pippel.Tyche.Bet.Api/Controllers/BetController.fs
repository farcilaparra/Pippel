namespace Pippel.Bet.Tyche.Api.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type BetController
    (
        logger: ILogger<BetController>,
        editBetAction: IEditBetAction,
        findOpenedMasterPoolsByGamblerAction: IFindOpenedMasterPoolsByGamblerAction,
        findMatchesByPoolAction: IFindMatchesByPoolAction,
        findBetsByPoolAction: IFindBetsByPoolAction,
        findOnPlayingMatchesByMasterPoolAction: IFindOnPlayingMatchesByMasterPoolAction
    ) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.AsyncGetOpened(gamblerID: Uuid) : Async<PoolReviewViewDto seq> =
        async {
            let! items = findOpenedMasterPoolsByGamblerAction.AsyncExecute(gamblerID)

            return
                items
                |> Seq.map (fun x -> x |> PoolReviewViewMapper.mapToView)
        }

    [<HttpGet("matches")>]
    member this.AsyncGetMatches(poolID: Uuid) : Async<MatchViewDto seq> =
        async {
            let! items = findMatchesByPoolAction.AsyncExecute poolID

            return
                items
                |> Seq.map (fun x -> x |> MatchViewMapper.mapToView)
        }

    [<HttpPut("edit")>]
    member this.AsyncEditBet(editingBetsDtos: EditingBetDto []) =
        async {
            let! items =
                editBetAction.AsyncExecute(
                    editingBetsDtos
                    |> Array.map (fun x -> x |> EditingBetViewMapper.mapFromView)
                )

            return
                items
                |> Seq.map (fun x -> x |> EditingBetViewMapper.mapToView)
        }

    [<HttpGet("position")>]
    member this.AsyncGetPositions(poolID: Uuid) : Async<BetPositionViewDto seq> =
        async {
            let! items = findBetsByPoolAction.AsyncExecute(poolID)

            return
                items
                |> Seq.map (fun x -> x |> BetPositionViewMapper.mapToView)
        }

    [<HttpGet("positionandonplayingmatches")>]
    member this.AsyncGetPositionsAndOnPlayingMatches(poolID: Uuid) : Async<BetPositionAndOnPlayingMatchViewDto> =
        async {
            let! positions = findBetsByPoolAction.AsyncExecute(poolID)
            let! onPlayingMatches = findOnPlayingMatchesByMasterPoolAction.AsyncExecute(poolID)

            return
                { BetPositionAndOnPlayingMatchViewDto.BetsPositions =
                      (positions
                       |> Seq.map (fun x -> x |> BetPositionViewMapper.mapToView))
                  OnPlayingMatches =
                      (onPlayingMatches
                       |> Seq.map (fun x -> x |> OnPlayingMatchViewMapper.mapToView)) }
        }
