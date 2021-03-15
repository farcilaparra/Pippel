namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type RoundPK = { RoundID: Uuid }

type RoundDomain =
    { ID: RoundPK
      MasterPoolID: Uuid
      Name: NotEmptyString100
      PointID: Uuid }
