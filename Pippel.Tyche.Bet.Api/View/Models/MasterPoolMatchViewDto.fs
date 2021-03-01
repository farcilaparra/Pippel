namespace Pippel.Tyche.Bet.Data.Models

open Pippel.Type

type MasterPoolMatchViewDto =
    { MasterPoolID: Uuid
      HomeTeamName: NotEmptyString
      AwayTeamName: NotEmptyString
      MatchDate: DateTime }
