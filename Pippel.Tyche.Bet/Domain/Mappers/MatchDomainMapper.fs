namespace Pippel.Tyche.Bet.Domain.Mappers

open System
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Type
open Pippel.Type

module MatchDomainMapper =

    let mapFromDomain (matchDomain: MatchDomain): MatchDao =
        { MatchDao.MatchID = matchDomain.ID.MatchID |> Uuid.value
          HomeTeamID = matchDomain.HomeTeamID |> Uuid.value
          AwayTeamID = matchDomain.AwayTeamID |> Uuid.value
          RoundID = matchDomain.RoundMatchID |> Uuid.value
          MatchDate = matchDomain.MatchDate |> DateTime.value
          HomeResult = matchDomain.HomeResult |> Number.nullableValue
          AwayResult = matchDomain.AwayResult |> Number.nullableValue
          Status = matchDomain.Status }

    let mapToDomain (matchDao: MatchDao): MatchDomain =
        try
            { ID = { MatchID = Uuid.From matchDao.MatchID }
              HomeTeamID = Uuid.From matchDao.HomeTeamID
              AwayTeamID = Uuid.From matchDao.AwayTeamID
              RoundMatchID = Uuid.From matchDao.RoundID
              MatchDate = DateTime.From matchDao.MatchDate
              HomeResult = Score.TryFromNullable matchDao.HomeResult
              AwayResult = Score.TryFromNullable matchDao.AwayResult
              Status = matchDao.Status }
        with :? ArgumentException as ex -> raise <| DomainValueException(ex.Message)
