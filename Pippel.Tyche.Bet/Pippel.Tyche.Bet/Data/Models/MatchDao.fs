namespace Pippel.Tyche.Bet.Data.Models

open System

type MatchStatus =
    | Pending = 0
    | Playing = 1
    | Played = 2
    | Suspended = 3
    | Canceled = 4

[<CLIMutable>]
type MatchDao =
    { ID: Guid
      HomeTeamID: Guid
      AwayTeamID: Guid
      RoundMatchID: Guid
      MatchDate: DateTime
      HomeResult: int Nullable
      AwayResult: int Nullable
      Status: MatchStatus }
