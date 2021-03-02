namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

module MatchViewMapper =

    let mapToView (matchViewDao: MatchViewDao) : MatchViewDto =
        { MatchID = matchViewDao.MatchID
          RoundID = matchViewDao.RoundID
          MasterPoolID = matchViewDao.MasterPoolID
          PoolID = matchViewDao.PoolID
          MatchStatus = matchViewDao.MatchStatus
          HomeTeamID = matchViewDao.HomeTeamID
          AwayTeamID = matchViewDao.AwayTeamID
          MatchDate = matchViewDao.MatchDate
          HomeTeamName = matchViewDao.HomeTeamName
          AwayTeamName = matchViewDao.AwayTeamName
          Point = matchViewDao.Point }
