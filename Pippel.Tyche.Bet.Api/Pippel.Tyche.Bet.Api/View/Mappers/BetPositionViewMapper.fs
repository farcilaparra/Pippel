namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models.Queries

type BetPositionViewMapper() =

    member this.Map(betPositionViewDao: BetPositionViewDao): BetPositionViewDto =
        { BetPositionViewDto.GroupBetID = betPositionViewDao.GroupBetID
          GamblerID = betPositionViewDao.GamblerID
          EnrollmentDate = betPositionViewDao.EnrollmentDate
          Point = betPositionViewDao.Point
          CurrentPosition = betPositionViewDao.CurrentPosition
          BeforePosition = betPositionViewDao.BeforePosition }
