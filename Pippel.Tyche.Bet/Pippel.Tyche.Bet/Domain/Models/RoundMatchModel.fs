namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type RoundMatch =
    { ID: Uuid
      GroupMatchID: Uuid
      Name: NonEmptyString }
