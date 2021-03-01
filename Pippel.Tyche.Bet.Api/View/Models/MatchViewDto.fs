namespace Pippel.Tyche.Bet.Api.Data.Models

open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type MatchViewDto =
    { MatchID: Uuid
      RoundID: Uuid
      MasterPoolID: Uuid
      PoolID: Uuid
      MatchStatus: MatchStatus
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      MatchDate: DateTime
      HomeTeamName: NotEmptyString
      AwayTeamName: NotEmptyString
      Point: PositiveInt option }
