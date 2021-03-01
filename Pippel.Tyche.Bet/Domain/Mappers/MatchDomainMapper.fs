namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module MatchDomainMapper =

    let mapFromDomain (matchDomain: MatchDomain) : MatchDao =
        { MatchDao.MatchID = matchDomain.ID.MatchID
          HomeTeamID = matchDomain.HomeTeamID
          AwayTeamID = matchDomain.AwayTeamID
          RoundID = matchDomain.RoundMatchID
          MatchDate = matchDomain.MatchDate
          HomeResult = matchDomain.HomeResult
          AwayResult = matchDomain.AwayResult
          Status = matchDomain.Status }

    let mapToDomain (matchDao: MatchDao) : MatchDomain =
        { ID = { MatchID = matchDao.MatchID }
          HomeTeamID = matchDao.HomeTeamID
          AwayTeamID = matchDao.AwayTeamID
          RoundMatchID = matchDao.RoundID
          MatchDate = matchDao.MatchDate
          HomeResult = matchDao.HomeResult
          AwayResult = matchDao.AwayResult
          Status = matchDao.Status }
