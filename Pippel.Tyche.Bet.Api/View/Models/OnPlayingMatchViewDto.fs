namespace Pippel.Tyche.Bet.Data.Models.Queries

open Pippel.Type

type OnPlayingMatchViewDto =
    { MatchID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      MatchDate: DateTime
      HomeTeamName: NotEmptyString
      AwayTeamName: NotEmptyString
      MasterPoolID: Uuid }
