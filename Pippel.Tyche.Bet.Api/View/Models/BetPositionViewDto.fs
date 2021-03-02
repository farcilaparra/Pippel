namespace Pippel.Tyche.Bet.Data.Models.Queries

open Pippel.Type

type BetPositionViewDto =
    { PoolID: Uuid
      GamblerID: Uuid
      EnrollmentDate: DateTime
      Point: PositiveInt option
      CurrentPosition: PositiveInt option
      BeforePosition: PositiveInt option }
