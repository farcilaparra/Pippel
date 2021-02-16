namespace Pippel.Tyche.Bet.Data.Models.Queries

open System

[<CLIMutable>]
type OnPlayingMatchViewDao =
    { MatchID: Guid
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeTeamName: string
      AwayTeamName: string
      GroupMatchID: Guid }
