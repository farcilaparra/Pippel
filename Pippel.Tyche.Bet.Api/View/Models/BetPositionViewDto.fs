namespace Pippel.Tyche.Bet.Data.Models.Queries

open System

type BetPositionViewDto =
    { PoolID: Guid
      GamblerID: Guid
      EnrollmentDate: DateTime
      Point: int Nullable
      CurrentPosition: int Nullable
      BeforePosition: int Nullable }
