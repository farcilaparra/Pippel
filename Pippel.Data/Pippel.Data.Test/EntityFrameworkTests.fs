module EntityFrameworkTests

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.EntityFrameworkCore
open Moq
open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Data.EntityFrameworkCore
open Pippel.Data.Test
open Xunit

/// Creates a context
let createContext () =
    new Context(DbContextOptionsBuilder<Context>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options)

/// Creates example data for products
let createProducts () =
    [| { Product.Id = "c1"
         Name = "Bottle of water 300 ml"
         Price = 0.5
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

[<Fact>]
let ``given an item that exist when an item is searched then the item is returned`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)
        let productToSearch = products.ElementAt(0)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! foundedProduct = repository.AsyncFindByKey([| productToSearch.Id |])

        Assert.Equal(productToSearch, foundedProduct)
    }

[<Fact>]
let ``given an item that exist when an action to find an item is executed then the item is returned`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()
        repositoryMock.Setup(fun x -> x.AsyncFindByKey(It.IsAny<obj []>()))
                      .Returns(Task.FromResult
                                   ({ Product.Id = "c1"
                                      Name = "Bottle of water 300 ml"
                                      Price = 0.5
                                      Supplier = "Lotto" })
                               |> Async.AwaitTask)
        |> ignore

        let mapperMock = Mock<IMapper<Product, Product>>()

        let findByKeyAction =
            FindByKeyAction(repositoryMock.Object, mapperMock.Object)

        let! foundedProduct = findByKeyAction.AsyncExecute([| "c1" |])

        repositoryMock.Verify((fun x -> x.AsyncFindByKey(It.IsAny<obj []>())), Times.Once())
    }

[<Fact>]
let ``given an item that doesn't exist when it's searched then an exception is raised`` () =
    let products = createProducts ()
    let context = createContextWithData (products)

    let repository =
        Repository<Product>(context) :> IRepository<Product>

    Assert.Throws<NotFoundException>(fun () ->
        repository.AsyncFindByKey([| "c7" |])
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
            repository.AsyncFind
                ({ QueryObject.Skip = skip
                   Take = take
                   Select = None
                   Where = None
                   GroupBy = None
                   OrderBy = None })

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
let ``given several items that exist when an action to query them is executed then a paged result is returned`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()
        repositoryMock.Setup(fun x -> x.AsyncFind(It.IsAny<QueryObject>()))
                      .Returns(Task.FromResult
                                   ({ Page.CurrentPage = 1
                                      ItemsCount = 1L
                                      PageCount = 1
                                      GroupCount = 1
                                      PageSize = 1
                                      Items =
                                          [| { Product.Id = "c1"
                                               Name = "Bottle of water 300 ml"
                                               Price = 0.5
                                               Supplier = "Lotto" } :> obj |]
                                          |> Array.toSeq })
                               |> Async.AwaitTask)
        |> ignore

        let mapperMock = Mock<IMapper<Product, Product>>()

        let findAction =
            FindAction(repositoryMock.Object, mapperMock.Object)

        let! page =
            findAction.AsyncExecute
                ({ QueryObject.Skip = 0
                   Take = 1
                   Select = None
                   Where = None
                   GroupBy = None
                   OrderBy = None })

        repositoryMock.Verify((fun x -> x.AsyncFind(It.IsAny<QueryObject>())), Times.Once())
    }

[<Fact>]
let ``given several items that exist when they are queried and filtered by any criteria then a paged result is returned with the items that meet the criteria`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let supplier = "Bavaria"

        let productsFilteredBefore =
            products.Where(fun x -> x.Supplier = supplier)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! page =
            repository.AsyncFind
                ({ QueryObject.Skip = 0
                   Take = products.Length
                   Select = None
                   Where = Some(String.Format("Supplier = \"{0}\"", supplier))
                   GroupBy = None
                   OrderBy = None })

        Assert.Equal(productsFilteredBefore.Count(), page.Items.Count())
        Assert.Equal(productsFilteredBefore.Count(), page.PageCount)

        for productFilteredAfter in page.Items do
            Assert.True(productsFilteredBefore.Contains(productFilteredAfter :?> Product))
    }

[<Fact>]
let ``given several items that exist when they are queried and ordered by any criteria then a paged result is returned with the ordered items`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productsOrderedBefore =
            products.OrderByDescending(fun x -> x.Price)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! page =
            repository.AsyncFind
                ({ QueryObject.Skip = 0
                   Take = products.Length
                   Select = None
                   Where = None
                   GroupBy = None
                   OrderBy = Some "Price desc" })

        let mutable i = 0

        for productOrderedAfter in page.Items do
            Assert.Equal(productsOrderedBefore.ElementAt(i), productOrderedAfter :?> Product)
            i <- i + 1
    }

[<Fact>]
let ``given several items that exist when they are queried and grouped by any criteria then a paged result is returned with the grouped items`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productsGroupedBefore =
            products.GroupBy(fun x -> x.Supplier)
                    .Select(fun x ->
                    {| Supplier = x.Key
                       Count = x.Count() |}) :?> IEnumerable<obj>

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! page =
            repository.AsyncFind
                ({ QueryObject.Skip = 0
                   Take = products.Length
                   Select = Some "new(Key as Supplier, Count() as Count)"
                   Where = None
                   GroupBy = Some "Supplier"
                   OrderBy = None })

        Assert.Equal(productsGroupedBefore.Count(), page.Items.Count())
        Assert.Equal(productsGroupedBefore.Count(), page.GroupCount)
        
        for item in page.Items do
            Assert.NotNull(item.GetType().GetProperty("Count").GetValue(item, null))

            let productGroupedAfter =
                {| Supplier = item.GetType().GetProperty("Supplier").GetValue(item, null) :?> string
                   Count = item.GetType().GetProperty("Count").GetValue(item, null) :?> int |}

            Assert.True(productsGroupedBefore.Contains(productGroupedAfter))
    }

[<Fact>]
let ``given an item that doesn't exist when it's mark to persist and the changes are saved then the item is persisted`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let countBeforeAdd = context.Products.Count()

        let productToAdd =
            { Product.Id = "c6"
              Name = "Red Bull 500 ml"
              Price = 3.0
              Supplier = "Bavaria" }

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! addedProducts = repository.AsyncAdd([| productToAdd |])
        let addedProduct = addedProducts.ElementAt(0)
        context.SaveChanges() |> ignore

        let countAfterAdd = context.Products.Count()

        Assert.Equal(productToAdd, addedProduct)

        Assert.Equal(countBeforeAdd + 1, countAfterAdd)
    }

[<Fact>]
let ``given an item that doesn't exist when an action to persist it is executed then the item is persisted`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()
        repositoryMock.Setup(fun x -> x.AsyncAdd(It.IsAny<Product seq>()))
                      .Returns(Task.FromResult
                                   ([| { Product.Id = "c1"
                                         Name = "Bottle of water 300 ml"
                                         Price = 0.5
                                         Supplier = "Lotto" } |]
                                    |> Array.toSeq)
                               |> Async.AwaitTask)
        |> ignore

        let mapperMock = Mock<IMapper<Product, Product>>()

        let unitOfWorkMock = Mock<IUnitOfWork>()

        let productToAdd =
            { Product.Id = "c6"
              Name = "Red Bull 500 ml"
              Price = 3.0
              Supplier = "Bavaria" }

        let addAction =
            AddAction(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object)

        let! addedProducts = addAction.AsyncExecute(([| productToAdd |] |> Array.toSeq))

        repositoryMock.Verify((fun x -> x.AsyncAdd(It.IsAny<Product seq>())), Times.Once())
        unitOfWorkMock.Verify((fun x -> x.SaveChanges()), Times.Once())
    }

[<Fact>]
let ``given an item that exist when it's mark to persist and the changes are saved then the item is persisted`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let productToAdd =
            { Product.Id = products.ElementAt(0).Id
              Name = "Red Bull 500 ml"
              Price = 3.0
              Supplier = "Bavaria" }

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        Assert.Throws<AlreadyExistException>(fun () ->
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

        let! updatedProducts = repository.AsyncUpdate([| productToUpdate |])
        let updatedProduct = updatedProducts.ElementAt(0)

        Assert.Equal(productToUpdate, updatedProduct)
    }
    
[<Fact>]
let ``given an item that exists when an action to update it is executed then then the item is updated`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()
        repositoryMock.Setup(fun x -> x.AsyncUpdate(It.IsAny<Product seq>()))
                      .Returns(Task.FromResult
                                   ([| { Product.Id = "c1"
                                         Name = "Bottle of water 300 ml"
                                         Price = 0.5
                                         Supplier = "Lotto" } |]
                                    |> Array.toSeq)
                               |> Async.AwaitTask)
        |> ignore

        let mapperMock = Mock<IMapper<Product, Product>>()

        let unitOfWorkMock = Mock<IUnitOfWork>()

        let productToUpdate =
            { Product.Id = "c1"
              Name = "Bottle of water 300 ml"
              Price = 0.5
              Supplier = "Lotto" }

        let updateAction = UpdateAction(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object)
        
        let! updatedProducts = updateAction.AsyncExecute([| productToUpdate |])
        
        repositoryMock.Verify((fun x -> x.AsyncUpdate(It.IsAny<Product seq>())), Times.Once())
        unitOfWorkMock.Verify((fun x -> x.SaveChanges()), Times.Once())
    }

[<Fact>]
let ``given an item that exists when it's marked to remove and the changes are saved then the item is removed`` () =
    async {
        let products = createProducts ()
        let context = createContextWithData (products)

        let countBeforeRemove = context.Products.Count()

        let productToRemove = products.ElementAt(0)

        let repository =
            Repository<Product>(context) :> IRepository<Product>

        let! removedProducts = repository.AsyncRemove([| [| productToRemove.Id :> obj |] |])
        let removedProduct = removedProducts.ElementAt(0)

        context.SaveChanges() |> ignore

        let countAfterRemove = context.Products.Count()

        Assert.Equal(productToRemove, removedProduct)

        Assert.Equal(countBeforeRemove - 1, countAfterRemove)
    }

[<Fact>]
let ``given an item that exists when an action to remove is executed then the item is removed`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()
        repositoryMock.Setup(fun x -> x.AsyncRemove(It.IsAny<obj [] seq>()))
                      .Returns(Task.FromResult
                                   ([| { Product.Id = "c1"
                                         Name = "Bottle of water 300 ml"
                                         Price = 0.5
                                         Supplier = "Lotto" } |]
                                    |> Array.toSeq)
                               |> Async.AwaitTask)
        |> ignore

        let mapperMock = Mock<IMapper<Product, Product>>()

        let unitOfWorkMock = Mock<IUnitOfWork>()
        
        let removeAction = RemoveAction(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object)

        let! removedProducts = removeAction.AsyncExecute([| [| "c1" :> obj |] |])
        
        repositoryMock.Verify((fun x -> x.AsyncRemove(It.IsAny<obj [] seq>())), Times.Once())
        unitOfWorkMock.Verify((fun x -> x.SaveChanges()), Times.Once())
    }
