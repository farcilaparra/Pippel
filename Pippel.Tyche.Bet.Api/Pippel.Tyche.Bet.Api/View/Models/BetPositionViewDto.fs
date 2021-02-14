namespace Pippel.Tyche.Bet.Data.Models.Queries

open System

[<CLIMutable>]
type BetPositionViewDto =
    { GroupBetID: Guid
      GamblerID: Guid
      EnrollmentDate: DateTime
      Point: int Nullable
      CurrentPosition: int Nullable
      BeforePosition: int Nullable }
