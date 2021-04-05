module AddPoolsTests

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Data
open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Type
open Xunit
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models

let createContext () =
    new Context(DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)

let createUnitOfWork (context: Context) = UnitOfWork(context) :> IUnitOfWork

let fillExampleData (context: Context) =
    let gamblersDaos =
        [| { GamblerDao.UserID = Guid("718d7467-383f-4094-81c4-5b104d7969aa") } |]

    context.Gamblers.AddRange gamblersDaos

    let masterPoolsDaos =
        [| { MasterPoolDao.MasterPoolID = Guid("1531b33f-cd41-48f2-a412-9f65436b51b2")
             Name = "Copa América 2021"
             StartDate = DateTime.Now
             EndDate = DateTime.Now } |]

    context.MasterPools.AddRange masterPoolsDaos

    context.SaveChanges() |> ignore

let createAddPoolsAction (context: Context) =
    let poolRepository =
        PoolRepositoryInDB(context) :> IPoolRepository

    let masterPoolRepository =
        MasterPoolRepositoryInDB(context) :> IMasterPoolRepository

    let gamblerRepository =
        GamblerRepositoryInDB(context) :> IGamblerRepository
    AddPoolsAction(poolRepository, masterPoolRepository, gamblerRepository) :> IAddPoolsAction

[<Fact>]
let ``given a pool when valid data when an action to add it is executed then the pools are added`` () =
    let context = createContext ()
    let unitOfWork = createUnitOfWork (context)
    fillExampleData context

    let poolsDomains =
        [| { PoolDomain.ID = { PoolPK.PoolID = Uuid.newUuid () }
             MasterPoolID = Uuid.From(context.MasterPools.First().MasterPoolID)
             OwnerGamblerID = Uuid.From(context.Gamblers.First().UserID)
             CreationDate = DateTime.now ()
             Name = NotEmptyString100.From "Copa América de Pippel 2021" } |]

    let poolsCountBefore = context.Pools.Count()

    let addPoolsAction = createAddPoolsAction context

    addPoolsAction.AsyncExecute poolsDomains
    |> Async.RunSynchronously
    |> ignore

    unitOfWork.SaveChanges()

    Assert.Equal(poolsCountBefore + 1, context.Pools.Count())

[<Fact>]
let ``given a pool with a master pool doesn't exist when an action to add it is executed then an exception is raised`` () =
    let context = createContext ()
    fillExampleData context

    let poolsDomains =
        [| { PoolDomain.ID = { PoolPK.PoolID = Uuid.newUuid () }
             MasterPoolID = Uuid.newUuid ()
             OwnerGamblerID = Uuid.From(context.Gamblers.First().UserID)
             CreationDate = DateTime.now ()
             Name = NotEmptyString100.From "Copa América de Pippel 2021" } |]

    let addPoolsAction = createAddPoolsAction context

    Assert.Throws<DomainException>(fun () ->
        addPoolsAction.AsyncExecute poolsDomains
        |> Async.RunSynchronously
        |> ignore)

[<Fact>]
let ``given a pool with a gmabler doesn't exist when an action to add it is executed then an exception is raised`` () =
    let context = createContext ()
    fillExampleData context

    let poolsDomains =
        [| { PoolDomain.ID = { PoolPK.PoolID = Uuid.newUuid () }
             MasterPoolID = Uuid.From(context.MasterPools.First().MasterPoolID)
             OwnerGamblerID = Uuid.newUuid ()
             CreationDate = DateTime.now ()
             Name = NotEmptyString100.From "Copa América de Pippel 2021" } |]

    let addPoolsAction = createAddPoolsAction context

    Assert.Throws<DomainException>(fun () ->
        addPoolsAction.AsyncExecute poolsDomains
        |> Async.RunSynchronously
        |> ignore)
