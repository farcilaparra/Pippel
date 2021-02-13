namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type GroupBetGambler =
    { GroupBetID: Uuid
      GamblerID: Uuid
      Role: int
      EnrollmentDate: DateTime }
