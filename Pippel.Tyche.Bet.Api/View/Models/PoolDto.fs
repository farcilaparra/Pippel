namespace Pippel.Tyche.Bet.Api.Data.Models

open System

type PoolDto =
    { PoolID: Guid Nullable
      MasterPoolID: Guid
      OwnerGamblerID: Guid
      Name: string }
