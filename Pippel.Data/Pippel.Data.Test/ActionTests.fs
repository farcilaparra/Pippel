module ActionTests

open System.Threading.Tasks
open NSubstitute
open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Data.Test
open Xunit

[<Fact>]
let ``given an item that exist when an action to find an item is executed then the item is returned`` () =
    async {
        let repository = Substitute.For<IRepository<Product>>()

        repository
            .AsyncFindByKey(Arg.Any<obj []>())
            .Returns(
                Task.FromResult(
                    { Product.ID = "c1"
                      Name = "Bottle of water 300 ml"
                      Price = 0.5
                      Supplier = "Lotto" }
                )
                |> Async.AwaitTask
            )
        |> ignore

        let! foundedProduct = repository |> asyncFindByKey [| "c1" |] id

        repository
            .Received()
            .AsyncFindByKey(Arg.Any<obj []>())
        |> ignore
    }


[<Fact>]
let ``given several items that exist when an action to query them is executed then a paged result is returned`` () =
    async {
        let repository = Substitute.For<IRepository<Product>>()

        (repository.AsyncFindWithPagination(Arg.Any<IQueryObject>()) (Arg.Any<int>()) (Arg.Any<int>()))
            .Returns(
                Task.FromResult(
                    { Page.CurrentPage = 1
                      ItemsCount = 1L
                      PageCount = 1
                      GroupCount = 1
                      PageSize = 1
                      Items =
                          [| { Product.ID = "c1"
                               Name = "Bottle of water 300 ml"
                               Price = 0.5
                               Supplier = "Lotto" } |]
                          |> Array.toSeq }
                )
                |> Async.AwaitTask
            )
        |> ignore

        let! page =
            repository
            |> asyncFindWithPagination<Product, Product, Product>
                ((DynamicQueryObject(
                    { DynamicQueryParam.Select = None
                      Where = None
                      GroupBy = None
                      OrderBy = None }
                )))
                0
                1
                id

        (repository.Received().AsyncFindWithPagination<Product>
            (Arg.Any<IQueryObject>())
            (Arg.Any<int>())
            (Arg.Any<int>()))
        |> ignore
    }

[<Fact>]
let ``given an item that doesn't exist when an action to persist it is executed then the item is persisted`` () =
    async {
        let repository = Substitute.For<IRepository<Product>>()

        repository
            .AsyncAdd(Arg.Any<Product seq>())
            .Returns(
                Task.FromResult(
                    [| { Product.ID = "c1"
                         Name = "Bottle of water 300 ml"
                         Price = 0.5
                         Supplier = "Lotto" } |]
                    |> Array.toSeq
                )
                |> Async.AwaitTask
            )
        |> ignore

        let productToAdd =
            { Product.ID = "c6"
              Name = "Red Bull 500 ml"
              Price = 3.0
              Supplier = "Bavaria" }

        let! addedProducts =
            repository
            |> asyncAdd ([| productToAdd |] |> Array.toSeq) id id

        repository
            .Received()
            .AsyncAdd(Arg.Any<Product seq>())
        |> ignore
    }

[<Fact>]
let ``given an item that exists when an action to update it is executed then then the item is updated`` () =
    async {
        let repository = Substitute.For<IRepository<Product>>()

        repository
            .AsyncUpdate(Arg.Any<Product seq>())
            .Returns(
                Task.FromResult(
                    [| { Product.ID = "c1"
                         Name = "Bottle of water 300 ml"
                         Price = 0.5
                         Supplier = "Lotto" } |]
                    |> Array.toSeq
                )
                |> Async.AwaitTask
            )
        |> ignore

        let productToUpdate =
            { Product.ID = "c1"
              Name = "Bottle of water 300 ml"
              Price = 0.5
              Supplier = "Lotto" }

        let! updatedProducts =
            repository
            |> asyncUpdate [| productToUpdate |] id id

        repository
            .Received()
            .AsyncUpdate(Arg.Any<Product seq>())
        |> ignore
    }

[<Fact>]
let ``given an item that exists when an action to remove is executed then the item is removed`` () =
    async {
        let repository = Substitute.For<IRepository<Product>>()

        repository
            .AsyncRemove(Arg.Any<obj [] seq>())
            .Returns(
                Task.FromResult(
                    [| { Product.ID = "c1"
                         Name = "Bottle of water 300 ml"
                         Price = 0.5
                         Supplier = "Lotto" } |]
                    |> Array.toSeq
                )
                |> Async.AwaitTask
            )
        |> ignore

        let! removedProducts = repository |> asyncRemove [| [| "c1" |] |] id

        repository
            .Received()
            .AsyncRemove(Arg.Any<obj [] seq>())
        |> ignore
    }
