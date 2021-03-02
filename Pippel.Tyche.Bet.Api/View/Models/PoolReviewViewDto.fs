namespace Pippel.Tyche.Bet.Api.Data.Models

open Pippel.Type

type PoolReviewViewDto =
    { PoolID: Uuid
      GamblerID: Uuid
      MasterPoolName: NotEmptyString
      CurrentPoint: PositiveInt
      CurrentPosition: PositiveInt
      BeforePosition: PositiveInt }
