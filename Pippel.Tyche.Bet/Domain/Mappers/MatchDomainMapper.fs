namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module MatchDomainMapper =

    let mapFromDomain (matchDomain: MatchDomain) : MatchDao =
        { MatchDao.MatchID = matchDomain.ID.MatchID |> Uuid.value
          HomeTeamID = matchDomain.HomeTeamID |> Uuid.value
          AwayTeamID = matchDomain.AwayTeamID |> Uuid.value
          RoundID = matchDomain.RoundMatchID |> Uuid.value
          MatchDate = matchDomain.MatchDate |> DateTime.value
          HomeResult =
              matchDomain.HomeResult
              |> PositiveInt.nullableValue
          AwayResult =
              matchDomain.AwayResult
              |> PositiveInt.nullableValue
          Status = matchDomain.Status }

    let mapToDomain (matchDao: MatchDao) : MatchDomain =
        { ID = { MatchID = matchDao.MatchID |> Uuid.from }
          HomeTeamID = matchDao.HomeTeamID |> Uuid.from
          AwayTeamID = matchDao.AwayTeamID |> Uuid.from
          RoundMatchID = matchDao.RoundID |> Uuid.from
          MatchDate = matchDao.MatchDate |> DateTime.from
          HomeResult = matchDao.HomeResult |> PositiveInt.tryFromNullable
          AwayResult = matchDao.AwayResult |> PositiveInt.tryFromNullable
          Status = matchDao.Status }
