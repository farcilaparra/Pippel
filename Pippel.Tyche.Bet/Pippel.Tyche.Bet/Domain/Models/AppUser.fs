namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type AppUser =
    { ID: Uuid
      Email: NonEmptyString
      Password: NonEmptyString }
