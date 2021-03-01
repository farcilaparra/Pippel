namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Data.Actions
open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateBetsAction(betRepository: IBetRepository) =

    interface IUpdateBetsAction with

        member this.AsyncExecute(bets: BetDomain seq) : Async<BetDomain seq> =
            async {
                return!
                    betRepository
                    |> asyncUpdate bets BetDomainMapper.mapFromDomain BetDomainMapper.mapToDomain
            }
