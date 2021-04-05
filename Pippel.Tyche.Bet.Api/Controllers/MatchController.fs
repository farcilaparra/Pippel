namespace Pippel.Tyche.Bet.Api.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type MatchController
    (
        logger: ILogger<MatchController>,
        findMatchesByMasterPoolAction: IFindMatchesByMasterPoolAction,
        findOnPlayingMatchesByMasterPoolAction: IFindOnPlayingMatchesByPoolAction
    ) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get(masterPoolID: Guid) =
        async {
            let! matches = findMatchesByMasterPoolAction.AsyncExecute(Uuid.From masterPoolID)

            return
                matches
                |> Seq.map (fun x -> x |> MasterPoolMatchViewMapper.mapToView)
        }

    [<HttpGet("onplaying")>]
    member this.GetOnPlayingMatches(poolID: Guid) =
        async {
            let! matches = findOnPlayingMatchesByMasterPoolAction.AsyncExecute(Uuid.From poolID)

            return
                matches
                |> Seq.map (fun x -> x |> OnPlayingMatchViewMapper.mapToView)
        }
