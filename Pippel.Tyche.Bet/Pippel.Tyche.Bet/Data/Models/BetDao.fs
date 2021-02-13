namespace Pippel.Tyche.Bet.Data.Models

open System
            
[<CLIMutable>]
type BetDao =
    { ID: Guid
      GroupBetID: Guid
      GamblerID: Guid
      MatchID: Guid
      HomeTeamValue: int
      AwayTeamValue: int }
