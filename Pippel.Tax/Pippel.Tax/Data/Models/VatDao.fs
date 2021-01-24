namespace Pippel.Tax.Data.Models

open System

[<CLIMutable>]
type VatDao =
    { ID: Guid
      Name: string
      Percentage: float }
            