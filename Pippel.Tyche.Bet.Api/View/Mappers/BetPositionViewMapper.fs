namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models.Queries

module BetPositionViewMapper =

    let mapToView (betPositionViewDao: BetPositionViewDao) : BetPositionViewDto =
        { BetPositionViewDto.PoolID = betPositionViewDao.PoolID
          GamblerID = betPositionViewDao.GamblerID
          EnrollmentDate = betPositionViewDao.EnrollmentDate
          Point = betPositionViewDao.Point
          CurrentPosition = betPositionViewDao.CurrentPosition
          BeforePosition = betPositionViewDao.BeforePosition }
