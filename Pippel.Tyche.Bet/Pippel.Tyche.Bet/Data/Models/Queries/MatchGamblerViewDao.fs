namespace Pippel.Tyche.Bet.Data.Models.Queries

open System

[<CLIMutable>]
type MatchGamblerViewDao =
    { GroupBetID: Guid
      GroupMatchID: Guid
      GamblerID: Guid
      GroupMatchName: string
      StartDate: DateTime
      EndDate: DateTime
      CurrentPoint: int
      CurrentPosition: int
      BeforePosition: int }
