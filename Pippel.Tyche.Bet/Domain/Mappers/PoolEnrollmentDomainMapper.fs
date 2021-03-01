namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module PoolEnrollmentDomainMapper =

    let mapFromDomain (poolEnrollmentDomain: PoolEnrollmentDomain) : PoolEnrollmentDao =
        { PoolEnrollmentDao.PoolID = poolEnrollmentDomain.ID.PoolID
          PoolEnrollmentDao.GamblerID = poolEnrollmentDomain.ID.GamblerID
          EnrollmentDate = poolEnrollmentDomain.EnrollmentDate }

    let mapToDomain (poolEnrollmentDao: PoolEnrollmentDao) : PoolEnrollmentDomain =
        { PoolEnrollmentDomain.ID =
              { PoolID = poolEnrollmentDao.PoolID
                GamblerID = poolEnrollmentDao.GamblerID }
          EnrollmentDate = poolEnrollmentDao.EnrollmentDate }
