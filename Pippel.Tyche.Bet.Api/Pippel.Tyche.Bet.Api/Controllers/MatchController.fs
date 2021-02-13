﻿namespace Pippel.Tyche.Bet.Api.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type MatchController(logger: ILogger<MatchController>,
                     findMatchesByGroupMatchAction: IFindMatchesByGroupMatchAction,
                     matchGroupViewMapper) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.Get(groupMatchID: Guid): Async<MatchGroupViewDto seq> =
        async {
            let! matches = findMatchesByGroupMatchAction.AsyncExecute(groupMatchID |> Uuid.createFromGuid)

            return
                matches
                |> Seq.map (fun x ->
                    x
                    |> (matchGroupViewMapper :> MatchGroupViewMapper)
                        .MapToMatchGroupView)
        }
