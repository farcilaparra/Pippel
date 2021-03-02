namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module BetDomainMapper =

    let mapFromDomain (betDomain: BetDomain) : BetDao =
        { PoolID = betDomain.ID.PoolID
          GamblerID = betDomain.ID.GamblerID
          MatchID = betDomain.ID.MatchID
          HomeTeamValue = betDomain.HomeTeamValue
          AwayTeamValue = betDomain.AwayTeamValue }

    let mapToDomain (betDao: BetDao) : BetDomain =
        { ID =
              { PoolID = betDao.PoolID
                GamblerID = betDao.GamblerID
                MatchID = betDao.MatchID }
          HomeTeamValue = betDao.HomeTeamValue
          AwayTeamValue = betDao.AwayTeamValue }
