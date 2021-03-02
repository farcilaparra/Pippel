namespace Pippel.Data.Test

open System
open Microsoft.EntityFrameworkCore

module DemoData =

    let createContext () =
        new Context(
            DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString()
            )
                .Options
        )

    let createProducts () =
        [| { Product.ID = "c1"
             Name = "Bottle of water 300 ml"
             Price = 1.1
             Supplier = "Lotto" }
           { Product.ID = "c2"
             Name = "Bottle of water 600 ml"
             Price = 0.9
             Supplier = "Lotto" }
           { Product.ID = "c3"
             Name = "Heineken beer 350 ml"
             Price = 1.0
             Supplier = "Amm" }
           { Product.ID = "c4"
             Name = "Poker beer"
             Price = 0.8
             Supplier = "Bavaria" }
           { Product.ID = "c5"
             Name = "Coca cola 1000 ml"
             Price = 1.1
             Supplier = "Bavaria" } |]

    let createContextWithData (products: Product []) =
        let context = createContext ()
        context.Products.AddRange(products)
        context.SaveChanges() |> ignore
        context
