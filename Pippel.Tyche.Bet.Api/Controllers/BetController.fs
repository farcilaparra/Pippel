namespace Pippel.Bet.Tyche.Api.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Core
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
        findOnPlayingMatchesByMasterPoolAction: IFindOnPlayingMatchesByPoolAction
    ) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.AsyncGetOpened(gamblerID: Guid, filter: string, skip: int Nullable, take: int Nullable) : Async<PoolReviewViewDto Page> =
        async {
            let! page =
                findOpenedMasterPoolsByGamblerAction.AsyncExecute
                    (gamblerID |> Uuid.from)
                    (filter |> NotEmptyString.tryFrom)
                    (skip |> PositiveInt.fromNullable)
                    (take |> PositiveInt.fromNullable)

            return page |> Page.map PoolReviewViewMapper.mapToView
        }

    [<HttpGet("matches")>]
    member this.AsyncGetMatches(poolID: Guid) : Async<MatchViewDto seq> =
        async {
            let! items = findMatchesByPoolAction.AsyncExecute(poolID |> Uuid.from)

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
    member this.AsyncGetPositions(poolID: Guid) : Async<BetPositionViewDto seq> =
        async {
            let! items = findBetsByPoolAction.AsyncExecute(poolID |> Uuid.from)

            return
                items
                |> Seq.map (fun x -> x |> BetPositionViewMapper.mapToView)
        }

    [<HttpGet("positionandonplayingmatches")>]
    member this.AsyncGetPositionsAndOnPlayingMatches(poolID: Guid) : Async<BetPositionAndOnPlayingMatchViewDto> =
        async {
            let poolID = poolID |> Uuid.from
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
