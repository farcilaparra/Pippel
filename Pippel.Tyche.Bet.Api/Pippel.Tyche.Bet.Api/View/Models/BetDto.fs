namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
[<CLIMutable>]
type BetDto =
    { ID: Guid
      GroupBetID: Guid
      GamblerID: Guid
      MatchID: Guid
      HomeTeamValue: Int32
      AwayTeamValue: Int32
      LastPosition: Int32 }
