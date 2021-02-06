namespace Pippel.Tyche.Bet.Data.Models.Queries

open System

[<CLIMutable>]
type MatchGamblerViewDao =
    { GroupBetID: Guid
      GroupMatchID: Guid
      OwnerGamblerID: Guid
      Name: string
      StartDate: DateTime
      EndDate: DateTime }
