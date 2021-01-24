namespace Pippel.Tyche.Bet.Data.Models

open System
            
[<CLIMutable>]
type BetDao =
    { ID: Guid
      GroupBetID: Guid
      GamblerID: Guid
      MatchID: Guid
      HomeTeamValue: Int32
      AwayTeamValue: Int32
      LastPosition: Int32 }
