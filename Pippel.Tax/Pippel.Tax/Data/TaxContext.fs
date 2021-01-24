namespace Pippel.Tax

open Microsoft.EntityFrameworkCore
open Pippel.Tax.Data.Models
open Pippel.Tax.Entities.Configurations

type TaxContext(options: DbContextOptions<TaxContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private _vats: DbSet<VatDao>

    member this.Vats
        with get () = this._vats
        and set v = this._vats <- v

    override this.OnModelCreating builder = builder.ApplyConfiguration(VatEntityTypeConfiguration()) |> ignore
