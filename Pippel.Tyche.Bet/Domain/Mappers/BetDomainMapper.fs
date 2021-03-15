namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Type
open Pippel.Type

module BetDomainMapper =

    let mapFromDomain (betDomain: BetDomain) : BetDao =
        { PoolID = betDomain.ID.PoolID |> Uuid.value
          GamblerID = betDomain.ID.GamblerID |> Uuid.value
          MatchID = betDomain.ID.MatchID |> Uuid.value
          HomeTeamValue = betDomain.HomeTeamValue |> Number.value
          AwayTeamValue = betDomain.AwayTeamValue |> Number.value }

    let mapToDomain (betDao: BetDao) : BetDomain =
        { ID =
              { PoolID = Uuid.From betDao.PoolID
                GamblerID = Uuid.From betDao.GamblerID
                MatchID = Uuid.From betDao.MatchID }
          HomeTeamValue = Score.From betDao.HomeTeamValue
          AwayTeamValue = Score.From betDao.AwayTeamValue }
