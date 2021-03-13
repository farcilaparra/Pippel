namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions
open Pippel.Type

type FindBetByKeyAction(betRepository: IBetRepository) =

    interface IFindBetByKeyAction with

        member this.AsyncExecute(id: BetPK) : Async<BetDomain> =
            betRepository
            |> asyncFindByKey
                [| id.PoolID |> Uuid.value
                   id.GamblerID |> Uuid.value
                   id.MatchID |> Uuid.value |]
                BetDomainMapper.mapToDomain
