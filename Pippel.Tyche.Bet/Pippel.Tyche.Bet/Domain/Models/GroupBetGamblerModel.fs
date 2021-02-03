namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type GroupBetGambler =
    { GroupBetID: Uuid
      GamblerID: Uuid
      IsAdmin: NonEmptyString
      EnrollmentDate: DateTime
      CurrentPoint: PositiveInt }
