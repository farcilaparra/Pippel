namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type GroupBetGamblerDao =
    { GroupBetID: Guid
      GamblerID: Guid
      IsAdmin: String
      EnrollmentDate: DateTime
      CurrentPoint: Int32 }
