namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindPoolEnrollmentByKeyAction(poolEnrollmentRepository: IPoolEnrollmentRepository) =

    interface IFindPoolEnrollmentByKeyAction with

        member this.AsyncExecute(id: PoolEnrollmentPK) : Async<PoolEnrollmentDomain> =
            poolEnrollmentRepository
            |> asyncFindByKey [| id.PoolID; id.GamblerID |] PoolEnrollmentDomainMapper.mapToDomain
