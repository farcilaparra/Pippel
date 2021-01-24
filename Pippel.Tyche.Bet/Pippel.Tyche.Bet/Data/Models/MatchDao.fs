namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type MatchDao =
    { ID: Guid
      HomeTeamID: Guid
      AwayTeamID: Guid
      RoundMatchID: Guid
      MatchDate: DateTime
      HomeResult: Int32
      AwayResult: Int32
      State: string }
            