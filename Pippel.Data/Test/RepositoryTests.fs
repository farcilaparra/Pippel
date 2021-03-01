module RepositoryTests

open System.Collections.Generic
open System.Linq
open Pippel.Core
open Pippel.Data
open Pippel.Data.EntityFrameworkCore
open Pippel.Data.Test
open Pippel.Data.Test.DemoData
open Xunit

[<Fact>]
let ``given an item that exist when an item is searched then the item is returned`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)
        let productToSearch = products.ElementAt(0)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! foundedProduct = repository.AsyncFindByKey [| productToSearch.ID |]

        Assert.Equal(productToSearch, foundedProduct)
    }

[<Fact>]
let ``given an item that doesn't exist when it's searched then an exception is raised`` () =
    let products = createProducts ()
    let context = createContextWithData (products)

    let repository =
        Repository<Product>(context) :> IRepository<Product>

    Assert.Throws<NotFoundException>
        (fun () ->
            repository.AsyncFindByKey [| "c7" |]
            |> Async.RunSynchronously
            |> ignore)
    |> ignore

[<Fact>]
let ``given several items that exist when they are queried then a paged result is returned`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let skip = 1
        let take = products.Length

        let! page =
            repository.AsyncFindWithPagination<Product>
                (DynamicQueryObject(
                    { DynamicQueryParam.Select = None
                      Where = None
                      GroupBy = None
                      OrderBy = None }
                ))
                skip
                take

        Assert.Equal(products.Length, int (page.ItemsCount))

        Assert.Equal(skip / take, page.CurrentPage)

        Assert.Equal(take, page.PageSize)

        if skip + take >= products.Length then
            Assert.Equal(products.Length - skip, page.PageCount)
            Assert.Equal(take - skip, page.Items.Count())
        else
            Assert.Equal(take, page.PageCount)
            Assert.Equal(take, page.Items.Count())
    }

[<Fact>]
let ``given several items that exist when they are queried and filtered by any criteria then a paged result is returned with the items that meet the criteria``
    ()
    =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let supplier = "Bavaria"

        let productsFilteredBefore =
            products.Where(fun x -> x.Supplier = supplier)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! page =
            repository.AsyncFindWithPagination
                (DynamicQueryObject(
                    { DynamicQueryParam.Select = None
                      Where = Some("Supplier = @0", [| supplier |])
                      GroupBy = None
                      OrderBy = None }
                ))
                0
                products.Length

        Assert.Equal(productsFilteredBefore.Count(), page.Items.Count())
        Assert.Equal(productsFilteredBefore.Count(), page.PageCount)

        for productFilteredAfter in page.Items do
            Assert.True(productsFilteredBefore.Contains(productFilteredAfter))
    }

[<Fact>]
let ``given several items that exist when they are queried and ordered by any criteria using a DynamicQueryObject then a paged result is returned with the ordered items``
    ()
    =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productsOrderedBefore =
            products
                .OrderByDescending(fun x -> x.Price)
                .ThenBy(fun x -> x.Supplier)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! page =
            repository.AsyncFindWithPagination
                (DynamicQueryObject(
                    { DynamicQueryParam.Select = None
                      Where = None
                      GroupBy = None
                      OrderBy = Some "Price desc, Supplier asc" }
                ))
                0
                products.Length

        let mutable i = 0

        for productOrderedAfter in page.Items do
            Assert.Equal(productsOrderedBefore.ElementAt(i), productOrderedAfter)
            i <- i + 1
    }

[<Fact>]
let ``given several items that exist when they are queried and grouped by any criteria then a paged result is returned with the grouped items``
    ()
    =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productsGroupedBefore =
            products
                .GroupBy(fun x -> x.Supplier)
                .Select(fun x ->
                    {| Supplier = x.Key
                       Count = x.Count() |})
            :?> IEnumerable<obj>

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! page =
            repository.AsyncFindWithPagination
                (DynamicQueryObject(
                    { DynamicQueryParam.Select = Some "new(Key as Supplier, Count() as Count)"
                      Where = None
                      GroupBy = Some "Supplier"
                      OrderBy = None }
                ))
                0
                products.Length

        Assert.Equal(productsGroupedBefore.Count(), page.Items.Count())
        Assert.Equal(productsGroupedBefore.Count(), page.GroupCount)

        for item in page.Items do
            Assert.NotNull(
                item
                    .GetType()
                    .GetProperty("Count")
                    .GetValue(item, null)
            )

            let productGroupedAfter =
                {| Supplier =
                       item
                           .GetType()
                           .GetProperty("Supplier")
                           .GetValue(item, null)
                       :?> string
                   Count =
                       item
                           .GetType()
                           .GetProperty("Count")
                           .GetValue(item, null)
                       :?> int |}

            Assert.True(productsGroupedBefore.Contains(productGroupedAfter))
    }

[<Fact>]
let ``given an item that doesn't exist when it's mark to persist and the changes are saved then the item is persisted``
    ()
    =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let countBeforeAdd = context.Products.Count()

        let productToAdd =
            { Product.ID = "c6"
              Name = "Red Bull 500 ml"
              Price = 3.0
              Supplier = "Bavaria" }

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! addedProducts = repository.AsyncAdd [| productToAdd |]
        let addedProduct = addedProducts.ElementAt(0)
        context.SaveChanges() |> ignore

        let countAfterAdd = context.Products.Count()

        Assert.Equal(productToAdd, addedProduct)

        Assert.Equal(countBeforeAdd + 1, countAfterAdd)
    }

[<Fact>]
let ``given an item that exist when it's mark to persist and the changes are saved then the item is persisted`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productToAdd =
            { Product.ID = products.ElementAt(0).ID
              Name = "Red Bull 500 ml"
              Price = 3.0
              Supplier = "Bavaria" }

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        Assert.Throws<AlreadyExistException>
            (fun () ->
                repository.AsyncAdd([| productToAdd |])
                |> Async.RunSynchronously
                |> ignore)
        |> ignore
    }

[<Fact>]
let ``given an item that exists when it's marked to update and the changes are saved then the item is updated`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productToUpdate =
            { products.ElementAt(0) with
                  Price = 2.0 }

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! updatedProducts = repository.AsyncUpdate [| productToUpdate |]
        let updatedProduct = updatedProducts.ElementAt(0)

        Assert.Equal(productToUpdate, updatedProduct)
    }

[<Fact>]
let ``given an item that exists when it's marked to remove and the changes are saved then the item is removed`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData products

        let countBeforeRemove = context.Products.Count()

        let productToRemove = products.ElementAt(0)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! removedProducts = repository.AsyncRemove [| [| productToRemove.ID |] |]
        let removedProduct = removedProducts.ElementAt(0)

        context.SaveChanges() |> ignore

        let countAfterRemove = context.Products.Count()

        Assert.Equal(productToRemove, removedProduct)

        Assert.Equal(countBeforeRemove - 1, countAfterRemove)
    }
