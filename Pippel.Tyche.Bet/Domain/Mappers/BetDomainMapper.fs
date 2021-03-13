namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module BetDomainMapper =

    let mapFromDomain (betDomain: BetDomain) : BetDao =
        { PoolID = betDomain.ID.PoolID |> Uuid.value
          GamblerID = betDomain.ID.GamblerID |> Uuid.value
          MatchID = betDomain.ID.MatchID |> Uuid.value
          HomeTeamValue = betDomain.HomeTeamValue |> PositiveInt.value
          AwayTeamValue = betDomain.AwayTeamValue |> PositiveInt.value }

    let mapToDomain (betDao: BetDao) : BetDomain =
        { ID =
              { PoolID = betDao.PoolID |> Uuid.from
                GamblerID = betDao.GamblerID |> Uuid.from
                MatchID = betDao.MatchID |> Uuid.from }
          HomeTeamValue = betDao.HomeTeamValue |> PositiveInt.from
          AwayTeamValue = betDao.AwayTeamValue |> PositiveInt.from }
