namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models

module RoundDomainMapper =

    let mapFromDomain (roundDomain: RoundDomain) : RoundDao =
        { RoundDao.RoundID = roundDomain.ID.RoundID
          MasterPoolID = roundDomain.MasterPoolID
          Name = roundDomain.Name
          PointID = roundDomain.PointID }

    let mapToDomain (roundDao: RoundDao) : RoundDomain =
        { ID = { RoundID = roundDao.RoundID }
          MasterPoolID = roundDao.MasterPoolID
          Name = roundDao.Name
          PointID = roundDao.PointID }
