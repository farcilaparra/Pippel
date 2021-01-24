namespace Pippel.Tax.Domain.Models

open Pippel.Type

type Vat =
    { ID: Uuid
      Name: NonEmptyString
      Percentage: Percentage }
