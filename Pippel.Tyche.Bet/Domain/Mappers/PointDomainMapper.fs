namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models

module PointDomainMapper =

    let mapFromDomain (pointDomain: PointDomain) : PointDao =
        { PointDao.PointID = pointDomain.ID.PointID
          HomeResultPoint = pointDomain.HomeResultPoint
          AwayResultPoint = pointDomain.AwayResultPoint
          DiferencePoint = pointDomain.DiferencePoint
          InvertedDiferentePoint = pointDomain.InvertedDiferentePoint
          WinOrDrawPoint = pointDomain.WinOrDrawPoint }

    let mapToDomain (pointDao: PointDao) : PointDomain =
        { ID = { PointID = pointDao.PointID }
          HomeResultPoint = pointDao.HomeResultPoint
          AwayResultPoint = pointDao.AwayResultPoint
          DiferencePoint = pointDao.DiferencePoint
          InvertedDiferentePoint = pointDao.InvertedDiferentePoint
          WinOrDrawPoint = pointDao.WinOrDrawPoint }
