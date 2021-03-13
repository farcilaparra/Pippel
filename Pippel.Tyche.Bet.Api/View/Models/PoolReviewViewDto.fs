namespace Pippel.Tyche.Bet.Api.Data.Models

open System

type PoolReviewViewDto =
    { PoolID: Guid
      GamblerID: Guid
      MasterPoolName: string
      CurrentPosition: int Nullable
      BeforePosition: int Nullable }
