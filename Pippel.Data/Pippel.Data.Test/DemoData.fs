namespace Pippel.Data.Test

open System
open Microsoft.EntityFrameworkCore

module DemoData =
    
    /// Creates a context
    let createContext () =
        new Context(DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options)

    /// Creates example data for products
    let createProducts () =
        [| { Product.Id = "c1"
             Name = "Bottle of water 300 ml"
             Price = 1.1
             Supplier = "Lotto" }
           { Product.Id = "c2"
             Name = "Bottle of water 600 ml"
             Price = 0.9
             Supplier = "Lotto" }
           { Product.Id = "c3"
             Name = "Heineken beer 350 ml"
             Price = 1.0
             Supplier = "Amm" }
           { Product.Id = "c4"
             Name = "Poker beer"
             Price = 0.8
             Supplier = "Bavaria" }
           { Product.Id = "c5"
             Name = "Coca cola 1000 ml"
             Price = 1.1
             Supplier = "Bavaria" } |]


    /// Creates a context with example data
    let createContextWithData (products: Product []) =
        let context = createContext ()
        context.Products.AddRange(products)
        context.SaveChanges() |> ignore
        context
