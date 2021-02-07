namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
open Pippel.Tyche.Bet.Data.Models

[<CLIMutable>]
type MatchViewDao =
    { MatchID: Guid
      RoundMatchID: Guid
      GroupMatchID: Guid
      GroupBetID: Guid
      MatchStatus: MatchStatus
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeTeamName: string
      AwayTeamName: string
      Point: int Nullable }
