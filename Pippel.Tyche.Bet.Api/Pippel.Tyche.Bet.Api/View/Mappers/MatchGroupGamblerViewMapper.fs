namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchGroupGamblerViewMapper() =

    member this.Map(matchGamblerViewDao: MatchGroupGamblerViewDao): MatchGroupGamblerViewDto =
        { MatchGroupGamblerViewDto.GroupBetID = matchGamblerViewDao.GroupBetID
          GamblerID = matchGamblerViewDao.GamblerID
          GroupMatchName = matchGamblerViewDao.GroupMatchName
          CurrentPoint = matchGamblerViewDao.CurrentPoint
          CurrentPosition = matchGamblerViewDao.CurrentPosition
          BeforePosition = matchGamblerViewDao.BeforePosition }
