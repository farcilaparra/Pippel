namespace Pippel.Tyche.Bet.Api.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type MatchController
    (
        logger: ILogger<MatchController>,
        findMatchesByMasterPoolAction: IFindMatchesByMasterPoolAction,
        findOnPlayingMatchesByMasterPoolAction: IFindOnPlayingMatchesByMasterPoolAction
    ) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get(masterPoolID: Uuid) : Async<MasterPoolMatchViewDto seq> =
        async {
            let! matches = findMatchesByMasterPoolAction.AsyncExecute(masterPoolID)

            return
                matches
                |> Seq.map (fun x -> x |> MasterPoolMatchViewMapper.mapToView)
        }

    [<HttpGet("onplaying")>]
    member this.GetOnPlayingMatches(poolID: Uuid) : Async<OnPlayingMatchViewDto seq> =
        async {
            let! matches = findOnPlayingMatchesByMasterPoolAction.AsyncExecute(poolID)

            return
                matches
                |> Seq.map (fun x -> x |> OnPlayingMatchViewMapper.mapToView)
        }
