namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type Team = { ID: Uuid;
              TeamName: NonEmptyString }
