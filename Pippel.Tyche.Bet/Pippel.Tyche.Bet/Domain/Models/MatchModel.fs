namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type Match =
    { ID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt
      AwayResult: PositiveInt
      Status: MatchStatus }
