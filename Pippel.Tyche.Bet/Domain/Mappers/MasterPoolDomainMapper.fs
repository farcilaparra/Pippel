namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module MasterPoolDomainMapper =

    let mapFromDomain (masterPoolDomain: MasterPoolDomain) : MasterPoolDao =
        { MasterPoolDao.MasterPoolID = masterPoolDomain.ID.MasterPoolID
          Name = masterPoolDomain.Name
          StartDate = masterPoolDomain.StartDate
          EndDate = masterPoolDomain.EndDate }

    let mapToDomain (masterPoolDao: MasterPoolDao) : MasterPoolDomain =
        { ID = { MasterPoolID = masterPoolDao.MasterPoolID }
          Name = masterPoolDao.Name
          StartDate = masterPoolDao.StartDate
          EndDate = masterPoolDao.EndDate }
