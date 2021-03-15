namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module PoolEnrollmentDomainMapper =

    let mapFromDomain (poolEnrollmentDomain: PoolEnrollmentDomain) : PoolEnrollmentDao =
        { PoolEnrollmentDao.PoolID = poolEnrollmentDomain.ID.PoolID |> Uuid.value
          PoolEnrollmentDao.GamblerID = poolEnrollmentDomain.ID.GamblerID |> Uuid.value
          EnrollmentDate =
              poolEnrollmentDomain.EnrollmentDate
              |> DateTime.value }

    let mapToDomain (poolEnrollmentDao: PoolEnrollmentDao) : PoolEnrollmentDomain =
        { PoolEnrollmentDomain.ID =
              { PoolID = Uuid.From poolEnrollmentDao.PoolID
                GamblerID = Uuid.From poolEnrollmentDao.GamblerID }
          EnrollmentDate = DateTime.From poolEnrollmentDao.EnrollmentDate }
