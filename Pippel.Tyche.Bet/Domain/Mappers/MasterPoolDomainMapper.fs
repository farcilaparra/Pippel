namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module MasterPoolDomainMapper =

    let mapFromDomain (masterPoolDomain: MasterPoolDomain) : MasterPoolDao =
        { MasterPoolDao.MasterPoolID = masterPoolDomain.ID.MasterPoolID |> Uuid.value
          Name = masterPoolDomain.Name |> String.value
          StartDate = masterPoolDomain.StartDate |> DateTime.value
          EndDate = masterPoolDomain.EndDate |> DateTime.value }

    let mapToDomain (masterPoolDao: MasterPoolDao) : MasterPoolDomain =
        { ID = { MasterPoolID = Uuid.From masterPoolDao.MasterPoolID }
          Name = NotEmptyString100.From masterPoolDao.Name
          StartDate = DateTime.From masterPoolDao.StartDate
          EndDate = DateTime.From masterPoolDao.EndDate }
