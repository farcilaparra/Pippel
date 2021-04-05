namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type PoolPK = { PoolID: Uuid }

type PoolDomain =
    { ID: PoolPK
      MasterPoolID: Uuid
      OwnerGamblerID: Uuid
      CreationDate: DateTime
      Name: NotEmptyString100 }
