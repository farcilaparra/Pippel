namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Type
open Pippel.Type

module PointDomainMapper =

    let mapFromDomain (pointDomain: PointDomain) : PointDao =
        { PointDao.PointID = pointDomain.ID.PointID |> Uuid.value
          HomeResultPoint = pointDomain.HomeResultPoint |> Number.value
          AwayResultPoint = pointDomain.AwayResultPoint |> Number.value
          DifferencePoint = pointDomain.DifferencePoint |> Number.value
          InvertedDifferencePoint =
              pointDomain.InvertedDifferencePoint
              |> Number.value
          WinOrDrawPoint = pointDomain.WinOrDrawPoint |> Number.value }

    let mapToDomain (pointDao: PointDao) : PointDomain =
        { ID = { PointID = Uuid.From pointDao.PointID }
          HomeResultPoint = ScoreWeight.From pointDao.HomeResultPoint
          AwayResultPoint = ScoreWeight.From pointDao.AwayResultPoint
          DifferencePoint = ScoreWeight.From pointDao.DifferencePoint
          InvertedDifferencePoint = ScoreWeight.From pointDao.InvertedDifferencePoint
          WinOrDrawPoint = ScoreWeight.From pointDao.WinOrDrawPoint }
