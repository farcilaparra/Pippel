namespace Pippel.Tyche.Bet.Data.Models.Queries

open System

[<CLIMutable>]
type BetPositionViewDao =
    { GroupBetID: Guid
      GamblerID: Guid
      EnrollmentDate: DateTime
      Point: int Nullable
      CurrentPosition: int Nullable
      BeforePosition: int Nullable }
