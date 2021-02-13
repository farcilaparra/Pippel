namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type GroupBetGamblerDao =
    { GroupBetID: Guid
      GamblerID: Guid
      Role: int
      EnrollmentDate: DateTime }
