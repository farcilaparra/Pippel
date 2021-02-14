namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchViewMapper() =

    member this.Map(matchViewDao: MatchViewDao): MatchViewDto =
        { MatchID = matchViewDao.MatchID
          RoundMatchID = matchViewDao.RoundMatchID
          GroupMatchID = matchViewDao.GroupMatchID
          GroupBetID = matchViewDao.GroupBetID
          MatchStatus = LanguagePrimitives.EnumToValue matchViewDao.MatchStatus
          HomeTeamID = matchViewDao.HomeTeamID
          AwayTeamID = matchViewDao.AwayTeamID
          MatchDate = matchViewDao.MatchDate
          HomeTeamName = matchViewDao.HomeTeamName
          AwayTeamName = matchViewDao.AwayTeamName
          Point = matchViewDao.Point }
