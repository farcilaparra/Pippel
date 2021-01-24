namespace Pippel.Tax.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tax
open Pippel.Tax.Data.Models

type VatRepositoryInDB(context: TaxContext) =
    inherit Repository<VatDao>(context)
    interface IVatRepository
