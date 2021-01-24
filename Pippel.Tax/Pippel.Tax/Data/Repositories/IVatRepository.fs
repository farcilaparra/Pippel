namespace Pippel.Tax.Data.Repositories

open Pippel.Data
open Pippel.Tax.Data.Models

[<Interface>]
type IVatRepository =
    inherit IRepository<VatDao>
