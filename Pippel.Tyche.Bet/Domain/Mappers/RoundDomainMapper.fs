namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Mapper
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

module RoundDomainMapper =

    let mapFromDomain (roundDomain: RoundDomain) : RoundDao =
        { RoundDao.RoundID = roundDomain.ID.RoundID |> Uuid.value
          MasterPoolID = roundDomain.MasterPoolID |> Uuid.value
          Name = roundDomain.Name |> String.value
          PointID = roundDomain.PointID |> Uuid.value }

    let mapToDomain (roundDao: RoundDao) : RoundDomain =
        tryMap {
            return
                { ID = { RoundID = Uuid.From roundDao.RoundID }
                  MasterPoolID = Uuid.From roundDao.MasterPoolID
                  Name = NotEmptyString100.From roundDao.Name
                  PointID = Uuid.From roundDao.PointID }
        }
