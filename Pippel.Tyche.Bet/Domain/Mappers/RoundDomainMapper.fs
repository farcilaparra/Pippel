namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

module RoundDomainMapper =

    let mapFromDomain (roundDomain: RoundDomain) : RoundDao =
        { RoundDao.RoundID = roundDomain.ID.RoundID |> Uuid.value
          MasterPoolID = roundDomain.MasterPoolID |> Uuid.value
          Name = roundDomain.Name |> NotEmptyString.value
          PointID = roundDomain.PointID |> Uuid.value }

    let mapToDomain (roundDao: RoundDao) : RoundDomain =
        { ID = { RoundID = roundDao.RoundID |> Uuid.from }
          MasterPoolID = roundDao.MasterPoolID |> Uuid.from
          Name = roundDao.Name |> NotEmptyString.from
          PointID = roundDao.PointID |> Uuid.from }
