namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type MatchPK =
    { MatchID: Uuid }

type MatchDomain =
    { ID: MatchPK
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt option
      AwayResult: PositiveInt option
      Status: MatchStatus }
