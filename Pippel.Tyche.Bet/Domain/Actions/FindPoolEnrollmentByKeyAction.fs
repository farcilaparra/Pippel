namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions
open Pippel.Type

type FindPoolEnrollmentByKeyAction(poolEnrollmentRepository: IPoolEnrollmentRepository) =

    interface IFindPoolEnrollmentByKeyAction with

        member this.AsyncExecute(id: PoolEnrollmentPK) : Async<PoolEnrollmentDomain> =
            poolEnrollmentRepository
            |> asyncFindByKey
                [| id.PoolID |> Uuid.value
                   id.GamblerID |> Uuid.value |]
                PoolEnrollmentDomainMapper.mapToDomain
