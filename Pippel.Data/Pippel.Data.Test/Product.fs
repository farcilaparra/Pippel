namespace Pippel.Data.Test

open Pippel.Core

[<CLIMutable>]
type Product =
    { Id: string
      Name: string
      Price: float
      Supplier: string }
