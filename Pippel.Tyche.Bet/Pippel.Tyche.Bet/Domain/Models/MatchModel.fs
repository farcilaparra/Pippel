namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type Match =
    { ID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt32
      AwayResult: PositiveInt32
      State: NonEmptyString }
