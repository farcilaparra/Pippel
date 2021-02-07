module ActionTests

open System
open System.Linq
open System.Threading.Tasks
open Microsoft.EntityFrameworkCore
open Moq
open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Data.Test
open Xunit


/// Creates a context
let createContext () =
    new Context(DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)


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
let ``given an item that exist when an action to find an item is executed then the item is returned`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()

        repositoryMock
            .Setup(fun x -> x.AsyncFindByKey(It.IsAny<obj []>()))
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
let ``given several items that exist when an action to query them is executed then a paged result is returned`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()

        repositoryMock
            .Setup(fun x -> x.AsyncFind(It.IsAny<IQueryObject>(), 0, 1))
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
            FindAndGetPagedResultAction(repositoryMock.Object, mapperMock.Object)

        let! page =
            findAction.AsyncExecute
                (DynamicQueryObject
                    ({ DynamicQueryParam.Select = None
                       Where = None
                       GroupBy = None
                       OrderBy = None }))
                0
                1

        repositoryMock.Verify((fun x -> x.AsyncFind(It.IsAny<IQueryObject>(), 0, 1)), Times.Once())
    }


[<Fact>]
let ``given an item that doesn't exist when an action to persist it is executed then the item is persisted`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()

        repositoryMock
            .Setup(fun x -> x.AsyncAdd(It.IsAny<Product seq>()))
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
let ``given an item that exists when an action to update it is executed then then the item is updated`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()

        repositoryMock
            .Setup(fun x -> x.AsyncUpdate(It.IsAny<Product seq>()))
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

        let updateAction =
            UpdateAction(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object)

        let! updatedProducts = updateAction.AsyncExecute([| productToUpdate |])

        repositoryMock.Verify((fun x -> x.AsyncUpdate(It.IsAny<Product seq>())), Times.Once())
        unitOfWorkMock.Verify((fun x -> x.SaveChanges()), Times.Once())
    }


[<Fact>]
let ``given an item that exists when an action to remove is executed then the item is removed`` () =
    async {
        let repositoryMock = Mock<IRepository<Product>>()

        repositoryMock
            .Setup(fun x -> x.AsyncRemove(It.IsAny<obj [] seq>()))
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

        let removeAction =
            RemoveAction(repositoryMock.Object, unitOfWorkMock.Object, mapperMock.Object)

        let! removedProducts = removeAction.AsyncExecute([| [| "c1" :> obj |] |])

        repositoryMock.Verify((fun x -> x.AsyncRemove(It.IsAny<obj [] seq>())), Times.Once())
        unitOfWorkMock.Verify((fun x -> x.SaveChanges()), Times.Once())
    }
