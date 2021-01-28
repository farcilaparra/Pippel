namespace Pippel.Tax.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Core
open Pippel.Tyche.Bet.Actions
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

[<ApiController>]
[<Route("[controller]")>]
type BetController(logger: ILogger<BetController>,
                   betRepository: IBetRepository,
                   betDomainMapper: BetDomainMapper,
                   betViewMapper: BetViewMapper) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetByKey(id: string): Async<BetDto> =
        async {
            let findBetByKeyAction =
                FindBetByKeyAction(betRepository, betDomainMapper)

            let! bet = findBetByKeyAction.AsyncExecute [| (id |> Uuid.create) |]

            return bet |> (betViewMapper :> IMapper<BetDto, Bet>).MapToSource
        }