namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module MasterPoolDomainMapper =

    let mapFromDomain (masterPoolDomain: MasterPoolDomain) : MasterPoolDao =
        { MasterPoolDao.MasterPoolID = masterPoolDomain.ID.MasterPoolID |> Uuid.value
          Name = masterPoolDomain.Name |> NotEmptyString.value
          StartDate = masterPoolDomain.StartDate |> DateTime.value
          EndDate = masterPoolDomain.EndDate |> DateTime.value }

    let mapToDomain (masterPoolDao: MasterPoolDao) : MasterPoolDomain =
        { ID = { MasterPoolID = masterPoolDao.MasterPoolID |> Uuid.from }
          Name = masterPoolDao.Name |> NotEmptyString.from
          StartDate = masterPoolDao.StartDate |> DateTime.from
          EndDate = masterPoolDao.EndDate |> DateTime.from }
