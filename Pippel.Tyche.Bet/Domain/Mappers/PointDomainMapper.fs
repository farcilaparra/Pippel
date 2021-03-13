namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module PointDomainMapper =

    let mapFromDomain (pointDomain: PointDomain) : PointDao =
        { PointDao.PointID = pointDomain.ID.PointID |> Uuid.value
          HomeResultPoint = pointDomain.HomeResultPoint |> PositiveInt.value
          AwayResultPoint = pointDomain.AwayResultPoint |> PositiveInt.value
          DifferencePoint = pointDomain.DiferencePoint |> PositiveInt.value
          InvertedDifferencePoint =
              pointDomain.InvertedDiferentePoint
              |> PositiveInt.value
          WinOrDrawPoint = pointDomain.WinOrDrawPoint |> PositiveInt.value }

    let mapToDomain (pointDao: PointDao) : PointDomain =
        { ID = { PointID = pointDao.PointID |> Uuid.from }
          HomeResultPoint = pointDao.HomeResultPoint |> PositiveInt.from
          AwayResultPoint = pointDao.AwayResultPoint |> PositiveInt.from
          DiferencePoint = pointDao.DifferencePoint |> PositiveInt.from
          InvertedDiferentePoint =
              pointDao.InvertedDifferencePoint
              |> PositiveInt.from
          WinOrDrawPoint = pointDao.WinOrDrawPoint |> PositiveInt.from }
