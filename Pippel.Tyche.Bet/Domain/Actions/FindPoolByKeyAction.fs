namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions
open Pippel.Type

type FindPoolByKeyAction(poolRepository: IPoolRepository) =

    interface IFindPoolByKeyAction with

        member this.AsyncExecute(id: PoolPK) : Async<PoolDomain> =
            poolRepository
            |> asyncFindByKey [| id.PoolID |> Uuid.value |] PoolDomainMapper.mapToDomain
