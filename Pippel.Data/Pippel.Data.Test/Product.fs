namespace Pippel.Data.Test

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type Product =
    { ID: string
      Name: string
      Price: float
      Supplier: string }

type ProductEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<Product> with

        override this.Configure(builder: EntityTypeBuilder<Product>) =
            builder.HasKey(fun x -> x.ID :> obj) |> ignore
