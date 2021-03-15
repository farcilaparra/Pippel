namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type MasterPoolPK =
    { MasterPoolID: Uuid }

type MasterPoolDomain =
    { ID: MasterPoolPK
      Name: NotEmptyString100
      StartDate: DateTime
      EndDate: DateTime }
