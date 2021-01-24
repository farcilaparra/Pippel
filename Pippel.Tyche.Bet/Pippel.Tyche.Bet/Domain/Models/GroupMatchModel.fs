namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type GroupMatch =
    { ID: Uuid
      Name: NonEmptyString
      StartDate: DateTime
      EndDate: DateTime }
