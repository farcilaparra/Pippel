namespace Pippel.Tyche.Bet.Api.Data.Models

open System
open Pippel.Tyche.Bet.Data.Models

type MatchViewDto =
    { MatchID: Guid
      RoundID: Guid
      MasterPoolID: Guid
      PoolID: Guid
      MatchStatus: MatchStatus
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeTeamName: string
      AwayTeamName: string
      Point: int Nullable }
