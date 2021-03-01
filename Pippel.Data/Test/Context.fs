namespace Pippel.Data.Test

open Microsoft.EntityFrameworkCore

type Context(options: DbContextOptions<Context>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private products: DbSet<Product>

    member this.Products
        with get () = this.products
        and set v = this.products <- v

    override this.OnModelCreating builder =
        builder.ApplyConfiguration(ProductEntityTypeConfiguration())
        |> ignore
