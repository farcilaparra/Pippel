namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Data.Actions
open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddBetsAction(betRepository: IBetRepository) =

    interface IAddBetsAction with

        member this.AsyncExecute(bets: BetDomain seq) : Async<BetDomain seq> =
            async {
                return!
                    betRepository
                    |> asyncAdd bets BetDomainMapper.mapFromDomain BetDomainMapper.mapToDomain
            }
