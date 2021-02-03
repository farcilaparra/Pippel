namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type Match =
    { ID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt
      AwayResult: PositiveInt
      State: NonEmptyString }
