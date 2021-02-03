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
                   groupMatchRepository: IGroupMatchRepository,
                   groupMatchDomainMapper: GroupMatchDomainMapper,
                   groupMatchViewMapper: GroupMatchViewMapper) =
    inherit ControllerBase()

    [<HttpGet("opened")>]
    member this.GetOpened(gamblerID: string): Async<GroupMatchDto seq> =
        async {
            let findOpenedGroupsMatchesAction =
                FindOpenedGroupsMatchesAction(groupMatchRepository, groupMatchDomainMapper)

            let! bets = findOpenedGroupsMatchesAction.AsyncExecute(gamblerID |> Uuid.create)

            return
                bets
                |> Seq.map (fun x ->
                    x
                    |> (groupMatchViewMapper :> IMapper<GroupMatchDto, GroupMatch>)
                        .MapToSource)
        }
