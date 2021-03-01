module EditBetTests

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Core
open Pippel.Data
open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Type
open Xunit
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models

let editingBetsWithEntityDoesNoExist : obj [] seq =
    seq {
        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "87d1ad03-fe37-491f-ae56-32b2388fc7be"
                              |> Uuid.fromString
                          GamblerID =
                              "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                              |> Uuid.fromString
                          PoolID =
                              "107d3c40-faff-4980-8041-8763b4f43d42"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]

        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "718d7467-383f-4094-81c4-5b104d7969aa"
                              |> Uuid.fromString
                          GamblerID =
                              "87d1ad03-fe37-491f-ae56-32b2388fc7be"
                              |> Uuid.fromString
                          PoolID =
                              "107d3c40-faff-4980-8041-8763b4f43d42"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]

        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "718d7467-383f-4094-81c4-5b104d7969aa"
                              |> Uuid.fromString
                          GamblerID =
                              "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                              |> Uuid.fromString
                          PoolID =
                              "87d1ad03-fe37-491f-ae56-32b2388fc7be"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]
    }

let editingBetsWithMatchStatusNonEqualToPlaying : obj [] seq =
    seq {
        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "a7a49483-0db7-46e7-b705-a76419b2351d"
                              |> Uuid.fromString
                          GamblerID =
                              "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                              |> Uuid.fromString
                          PoolID =
                              "107d3c40-faff-4980-8041-8763b4f43d42"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]

        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "aff84e74-a475-4199-842e-f5976cc8effe"
                              |> Uuid.fromString
                          GamblerID =
                              "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                              |> Uuid.fromString
                          PoolID =
                              "107d3c40-faff-4980-8041-8763b4f43d42"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]

        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "e46e05d6-7416-467d-b752-838b0ce8d2dd"
                              |> Uuid.fromString
                          GamblerID =
                              "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                              |> Uuid.fromString
                          PoolID =
                              "107d3c40-faff-4980-8041-8763b4f43d42"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]

        yield
            [| [| { BetDomain.ID =
                        { MatchID =
                              "ac725ffb-c490-4625-b191-edc82a979ce5"
                              |> Uuid.fromString
                          GamblerID =
                              "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                              |> Uuid.fromString
                          PoolID =
                              "107d3c40-faff-4980-8041-8763b4f43d42"
                              |> Uuid.fromString }
                    HomeTeamValue = 0 |> PositiveInt.from
                    AwayTeamValue = 1 |> PositiveInt.from } |] |]
    }

let createContext () =
    new Context(
        DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(
            Guid.NewGuid().ToString()
        )
            .Options
    )

let createUnitOfWork (context: Context) = UnitOfWork(context) :> IUnitOfWork

let createExampleDataForBet (context: Context) =
    let matchDaos =
        [| { MatchDao.MatchID =
                 "718d7467-383f-4094-81c4-5b104d7969aa"
                 |> Uuid.fromString
             HomeTeamID =
                 "1531b33f-cd41-48f2-a412-9f65436b51b2"
                 |> Uuid.fromString
             AwayTeamID =
                 "a6edddde-cc90-4f6b-a24f-a5d7de79ab6f"
                 |> Uuid.fromString
             RoundID =
                 "9873ae81-734c-4fbb-9d9d-3d1d731a9e91"
                 |> Uuid.fromString
             MatchDate = DateTime.now
             HomeResult = None
             AwayResult = None
             Status = MatchStatus.Pending }
           { MatchDao.MatchID =
                 "43c2483f-9b3e-48b8-978c-99e07d2f29d2"
                 |> Uuid.fromString
             HomeTeamID =
                 "d2237e14-4cc9-4adb-8a28-c86862878573"
                 |> Uuid.fromString
             AwayTeamID =
                 "783152e7-d507-49d5-ab1e-4e4f92ac5b40"
                 |> Uuid.fromString
             RoundID =
                 Guid("dfffe00e-9e30-4de0-9e24-a864b3545386")
                 |> Uuid.from
             MatchDate = DateTime.now
             HomeResult = None
             AwayResult = None
             Status = MatchStatus.Pending }
           { MatchDao.MatchID =
                 "a7a49483-0db7-46e7-b705-a76419b2351d"
                 |> Uuid.fromString
             HomeTeamID =
                 "0a11711f-802e-4ac6-bd4c-528fb8c0a2c6"
                 |> Uuid.fromString
             AwayTeamID =
                 "2ecf47ce-1c65-4907-a9f7-ca96a442ed8d"
                 |> Uuid.fromString
             RoundID =
                 Guid("ac5de02f-0469-4e5f-ba4b-f6deb6f0727d")
                 |> Uuid.from
             MatchDate = DateTime.now
             HomeResult = None
             AwayResult = None
             Status = MatchStatus.Playing }
           { MatchDao.MatchID =
                 "aff84e74-a475-4199-842e-f5976cc8effe"
                 |> Uuid.fromString
             HomeTeamID =
                 "b9876e70-b950-4996-8182-fb09fe2e6470"
                 |> Uuid.fromString
             AwayTeamID =
                 "f2645c3b-7d01-40c2-be9e-bd89ffc42218"
                 |> Uuid.fromString
             RoundID =
                 Guid("f1af4f6c-9c28-4fcb-a035-f47c6006558d")
                 |> Uuid.from
             MatchDate = DateTime.now
             HomeResult = Some(2 |> PositiveInt.from)
             AwayResult = Some(1 |> PositiveInt.from)
             Status = MatchStatus.Played }
           { MatchDao.MatchID =
                 "e46e05d6-7416-467d-b752-838b0ce8d2dd"
                 |> Uuid.fromString
             HomeTeamID =
                 "99f563ef-1af5-4be9-8cd0-7bc9efd6b01a"
                 |> Uuid.fromString
             AwayTeamID =
                 "23d77693-76fe-4952-9b02-89af9bc752e2"
                 |> Uuid.fromString
             RoundID =
                 Guid("68365a26-a315-4d1f-accd-9c17caf54a73")
                 |> Uuid.from
             MatchDate = DateTime.now
             HomeResult = None
             AwayResult = None
             Status = MatchStatus.Suspended }
           { MatchDao.MatchID =
                 "ac725ffb-c490-4625-b191-edc82a979ce5"
                 |> Uuid.fromString
             HomeTeamID =
                 "bb3c0ff6-02d6-49a5-a565-d98e558c162e"
                 |> Uuid.fromString
             AwayTeamID =
                 "d94e19c2-fec1-4326-a5bd-d2709230fedd"
                 |> Uuid.fromString
             RoundID =
                 Guid("79b7c074-2559-4926-a833-75c2a7efb4d5")
                 |> Uuid.from
             MatchDate = DateTime.now
             HomeResult = None
             AwayResult = None
             Status = MatchStatus.Canceled } |]

    context.Matches.AddRange matchDaos

    let poolEnrollmentDaos =
        [| { PoolEnrollmentDao.PoolID =
                 "107d3c40-faff-4980-8041-8763b4f43d42"
                 |> Uuid.fromString
             GamblerID =
                 "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                 |> Uuid.fromString
             EnrollmentDate = DateTime.now } |]

    context.GroupsBetsGamblers.AddRange poolEnrollmentDaos

    let betDaos =
        [| { BetDao.PoolID =
                 "107d3c40-faff-4980-8041-8763b4f43d42"
                 |> Uuid.fromString
             GamblerID =
                 "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                 |> Uuid.fromString
             MatchID =
                 "718d7467-383f-4094-81c4-5b104d7969aa"
                 |> Uuid.fromString
             HomeTeamValue = 1 |> PositiveInt.from
             AwayTeamValue = 0 |> PositiveInt.from } |]

    context.Bets.AddRange betDaos

    context.SaveChanges() |> ignore

let createEditBetAction (context: Context) =
    let poolEnrollmentRepository =
        PoolEnrollmentRepositoryInDB(context) :> IPoolEnrollmentRepository

    let matchRepository =
        MatchRepositoryInDB(context) :> IMatchRepository

    let betRepository =
        BetRepositoryInDB(context) :> IBetRepository

    EditBetAction(
        FindPoolEnrollmentByKeyAction(poolEnrollmentRepository),
        FindMatchByKeyAction(matchRepository),
        FindBetByKeyAction(betRepository),
        UpdateBetsAction(betRepository),
        AddBetsAction(betRepository)
    )
    :> IEditBetAction

[<Fact>]
let ``given several bets when an action to edit them is executed then the bets are edited`` () =
    async {

        let context = createContext ()
        let unitOfWork = createUnitOfWork (context)

        createExampleDataForBet (context)

        let betsDomain =
            [| { BetDomain.ID =
                     { MatchID =
                           "718d7467-383f-4094-81c4-5b104d7969aa"
                           |> Uuid.fromString
                       GamblerID =
                           "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                           |> Uuid.fromString
                       PoolID =
                           "107d3c40-faff-4980-8041-8763b4f43d42"
                           |> Uuid.fromString }
                 HomeTeamValue = 0 |> PositiveInt.from
                 AwayTeamValue = 1 |> PositiveInt.from }
               { BetDomain.ID =
                     { MatchID =
                           "43c2483f-9b3e-48b8-978c-99e07d2f29d2"
                           |> Uuid.fromString
                       GamblerID =
                           "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                           |> Uuid.fromString
                       PoolID =
                           "107d3c40-faff-4980-8041-8763b4f43d42"
                           |> Uuid.fromString }
                 HomeTeamValue = 2 |> PositiveInt.from
                 AwayTeamValue = 1 |> PositiveInt.from } |]

        let betsCountBefore = context.Bets.Count()

        let editBetAction = createEditBetAction context

        let! editedBetsDomain = editBetAction.AsyncExecute betsDomain

        unitOfWork.SaveChanges()

        Assert.Equal(betsCountBefore + 1, context.Bets.Count())
    }

[<Theory>]
[<MemberData(nameof (editingBetsWithEntityDoesNoExist))>]
let ``given a bet wich has an item doesn't exist when an action to edit them is executed then a NotFoundException is raised``
    (betsDomain: BetDomain [])
    =
    async {

        let context = createContext ()

        createExampleDataForBet (context)

        let betsCountBefore = context.Bets.Count()

        let editBetAction = createEditBetAction context

        Assert.Throws<NotFoundException>
            (fun () ->
                editBetAction.AsyncExecute betsDomain
                |> Async.RunSynchronously
                |> ignore)
        |> ignore
    }

[<Theory>]
[<MemberData(nameof (editingBetsWithMatchStatusNonEqualToPlaying))>]
let ``given a bet wich has an match non equal to pending when an action to edit them is executed then a EditingBetNotAllowedException is raised``
    (betsDomain: BetDomain [])
    =
    async {

        let context = createContext ()

        createExampleDataForBet (context)

        let editBetAction = createEditBetAction context

        Assert.Throws<EditingBetNotAllowedException>
            (fun () ->
                editBetAction.AsyncExecute betsDomain
                |> Async.RunSynchronously
                |> ignore)
        |> ignore
    }
