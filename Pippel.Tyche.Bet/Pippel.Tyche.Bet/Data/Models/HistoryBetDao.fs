namespace Pippel.Tyche.Bet.Data.Models

open System
            
[<CLIMutable>]
type HistoryBetDao =
    { ID: Guid
      BetID: Guid
      HomeTeamValue: Int32
      AwayTeamValue: Int32
      CreationDate: DateTime }
