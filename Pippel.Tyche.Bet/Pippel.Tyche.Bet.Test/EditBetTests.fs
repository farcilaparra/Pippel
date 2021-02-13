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
open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Type
open Xunit
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Xunit

let editingBetsWithEntityDoesNoExist: obj [] seq =
    seq {
        yield
            [| [| { EditingBet.MatchID =
                        "87d1ad03-fe37-491f-ae56-32b2388fc7be"
                        |> Uuid.create
                    GamblerID =
                        "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                        |> Uuid.create
                    GroupBetID =
                        "107d3c40-faff-4980-8041-8763b4f43d42"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]

        yield
            [| [| { EditingBet.MatchID =
                        "718d7467-383f-4094-81c4-5b104d7969aa"
                        |> Uuid.create
                    GamblerID =
                        "87d1ad03-fe37-491f-ae56-32b2388fc7be"
                        |> Uuid.create
                    GroupBetID =
                        "107d3c40-faff-4980-8041-8763b4f43d42"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]

        yield
            [| [| { EditingBet.MatchID =
                        "718d7467-383f-4094-81c4-5b104d7969aa"
                        |> Uuid.create
                    GamblerID =
                        "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                        |> Uuid.create
                    GroupBetID =
                        "87d1ad03-fe37-491f-ae56-32b2388fc7be"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]
    }

let editingBetsWithMatchStatusNonEqualToPlaying: obj [] seq =
    seq {
        yield
            [| [| { EditingBet.MatchID =
                        "a7a49483-0db7-46e7-b705-a76419b2351d"
                        |> Uuid.create
                    GamblerID =
                        "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                        |> Uuid.create
                    GroupBetID =
                        "107d3c40-faff-4980-8041-8763b4f43d42"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]

        yield
            [| [| { EditingBet.MatchID =
                        "aff84e74-a475-4199-842e-f5976cc8effe"
                        |> Uuid.create
                    GamblerID =
                        "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                        |> Uuid.create
                    GroupBetID =
                        "107d3c40-faff-4980-8041-8763b4f43d42"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]

        yield
            [| [| { EditingBet.MatchID =
                        "e46e05d6-7416-467d-b752-838b0ce8d2dd"
                        |> Uuid.create
                    GamblerID =
                        "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                        |> Uuid.create
                    GroupBetID =
                        "107d3c40-faff-4980-8041-8763b4f43d42"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]

        yield
            [| [| { EditingBet.MatchID =
                        "ac725ffb-c490-4625-b191-edc82a979ce5"
                        |> Uuid.create
                    GamblerID =
                        "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                        |> Uuid.create
                    GroupBetID =
                        "107d3c40-faff-4980-8041-8763b4f43d42"
                        |> Uuid.create
                    HomeValue = 0 |> PositiveInt.create
                    AwayValue = 1 |> PositiveInt.create } |] |]
    }

let createContext () =
    new Context(DbContextOptionsBuilder<Context>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options)

let createExampleDataForEditBet (context: Context) =
    let matches =
        [| { MatchDao.ID = Guid("718d7467-383f-4094-81c4-5b104d7969aa")
             HomeTeamID = Guid("1531b33f-cd41-48f2-a412-9f65436b51b2")
             AwayTeamID = Guid("a6edddde-cc90-4f6b-a24f-a5d7de79ab6f")
             RoundMatchID = Guid("9873ae81-734c-4fbb-9d9d-3d1d731a9e91")
             MatchDate = DateTime.Now
             HomeResult = Nullable<int>()
             AwayResult = Nullable<int>()
             Status = MatchStatus.Pending }
           { MatchDao.ID = Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
             HomeTeamID = Guid("d2237e14-4cc9-4adb-8a28-c86862878573")
             AwayTeamID = Guid("783152e7-d507-49d5-ab1e-4e4f92ac5b40")
             RoundMatchID = Guid("dfffe00e-9e30-4de0-9e24-a864b3545386")
             MatchDate = DateTime.Now
             HomeResult = Nullable<int>()
             AwayResult = Nullable<int>()
             Status = MatchStatus.Pending }
           { MatchDao.ID = Guid("a7a49483-0db7-46e7-b705-a76419b2351d")
             HomeTeamID = Guid("0a11711f-802e-4ac6-bd4c-528fb8c0a2c6")
             AwayTeamID = Guid("2ecf47ce-1c65-4907-a9f7-ca96a442ed8d")
             RoundMatchID = Guid("ac5de02f-0469-4e5f-ba4b-f6deb6f0727d")
             MatchDate = DateTime.Now
             HomeResult = Nullable<int>()
             AwayResult = Nullable<int>()
             Status = MatchStatus.Playing }
           { MatchDao.ID = Guid("aff84e74-a475-4199-842e-f5976cc8effe")
             HomeTeamID = Guid("b9876e70-b950-4996-8182-fb09fe2e6470")
             AwayTeamID = Guid("f2645c3b-7d01-40c2-be9e-bd89ffc42218")
             RoundMatchID = Guid("f1af4f6c-9c28-4fcb-a035-f47c6006558d")
             MatchDate = DateTime.Now
             HomeResult = Nullable<int>(2)
             AwayResult = Nullable<int>(1)
             Status = MatchStatus.Played }
           { MatchDao.ID = Guid("e46e05d6-7416-467d-b752-838b0ce8d2dd")
             HomeTeamID = Guid("99f563ef-1af5-4be9-8cd0-7bc9efd6b01a")
             AwayTeamID = Guid("23d77693-76fe-4952-9b02-89af9bc752e2")
             RoundMatchID = Guid("68365a26-a315-4d1f-accd-9c17caf54a73")
             MatchDate = DateTime.Now
             HomeResult = Nullable<int>()
             AwayResult = Nullable<int>()
             Status = MatchStatus.Suspended }
           { MatchDao.ID = Guid("ac725ffb-c490-4625-b191-edc82a979ce5")
             HomeTeamID = Guid("bb3c0ff6-02d6-49a5-a565-d98e558c162e")
             AwayTeamID = Guid("d94e19c2-fec1-4326-a5bd-d2709230fedd")
             RoundMatchID = Guid("79b7c074-2559-4926-a833-75c2a7efb4d5")
             MatchDate = DateTime.Now
             HomeResult = Nullable<int>()
             AwayResult = Nullable<int>()
             Status = MatchStatus.Canceled } |]

    context.Matches.AddRange matches

    let groupsBetsGamblers =
        [| { GroupBetGamblerDao.GroupBetID = Guid("107d3c40-faff-4980-8041-8763b4f43d42")
             GamblerID = Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
             Role = 0
             EnrollmentDate = DateTime.Now } |]

    context.GroupsBetsGamblers.AddRange groupsBetsGamblers

    let bets =
        [| { BetDao.ID = Guid("f95e47d1-d3f9-424a-baea-f4cf3e9584c4")
             GroupBetID = Guid("107d3c40-faff-4980-8041-8763b4f43d42")
             GamblerID = Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
             MatchID = Guid("718d7467-383f-4094-81c4-5b104d7969aa")
             HomeTeamValue = 1
             AwayTeamValue = 0 } |]

    context.Bets.AddRange bets

    context.SaveChanges() |> ignore

let createEditBetAction (context: Context) =
    let groupBetGamblerRepository =
        GroupBetGamblerRepositoryInDB(context) :> IGroupBetGamblerRepository

    let matchRepository =
        MatchRepositoryInDB(context) :> IMatchRepository

    let betRepository =
        BetRepositoryInDB(context) :> IBetRepository

    let historyBetRepository =
        HistoryBetRepositoryInDB(context) :> IHistoryBetRepository

    let groupBetGamblerMapper =
        GroupBetGamblerDomainMapper() :> IMapper<GroupBetGambler, GroupBetGamblerDao>

    let matchMapper =
        MatchDomainMapper() :> IMapper<Match, MatchDao>

    let betMapper =
        BetDomainMapper() :> IMapper<Bet, BetDao>

    let historyBetMapper =
        HistoryBetDomainMapper() :> IMapper<HistoryBet, HistoryBetDao>

    let unitOfWork = UnitOfWork(context) :> IUnitOfWork

    EditBetAction
        (FindGroupBetGamblerByKeyAction(groupBetGamblerRepository, groupBetGamblerMapper),
         FindMatchByKeyAction(matchRepository, matchMapper),
         FindBetByKeyAction(betRepository, betMapper),
         UpdateBetsAction
             (betRepository,
              AddHistoryBetsAction(historyBetRepository, unitOfWork, historyBetMapper),
              unitOfWork,
              betMapper),
         AddBetsAction
             (betRepository,
              AddHistoryBetsAction(historyBetRepository, unitOfWork, historyBetMapper),
              unitOfWork,
              betMapper),
         unitOfWork) :> IEditBetAction

[<Fact>]
let ``given several bets when an action to edit them is executed then the bets are edited`` () =
    async {

        let context = createContext ()

        createExampleDataForEditBet (context)

        let editingBets =
            [| { EditingBet.MatchID =
                     "718d7467-383f-4094-81c4-5b104d7969aa"
                     |> Uuid.create
                 GamblerID =
                     "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                     |> Uuid.create
                 GroupBetID =
                     "107d3c40-faff-4980-8041-8763b4f43d42"
                     |> Uuid.create
                 HomeValue = 0 |> PositiveInt.create
                 AwayValue = 1 |> PositiveInt.create }
               { EditingBet.MatchID =
                     "43c2483f-9b3e-48b8-978c-99e07d2f29d2"
                     |> Uuid.create
                 GamblerID =
                     "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
                     |> Uuid.create
                 GroupBetID =
                     "107d3c40-faff-4980-8041-8763b4f43d42"
                     |> Uuid.create
                 HomeValue = 2 |> PositiveInt.create
                 AwayValue = 1 |> PositiveInt.create } |]

        let betsCountBefore = context.Bets.Count()

        let editBetAction = createEditBetAction context

        let! editedBets = editBetAction.AsyncExecute editingBets

        Assert.Equal(betsCountBefore + 1, context.Bets.Count())
        Assert.Equal(editingBets.Count(), context.HistoriresBets.Count())
    }

[<Theory>]
[<MemberData(nameof (editingBetsWithEntityDoesNoExist))>]
let ``given a bet wich has an item doesn't exist when an action to edit them is executed then a NotFoundException is raised`` (editingBets: EditingBet []) =
    async {

        let context = createContext ()

        createExampleDataForEditBet (context)

        let betsCountBefore = context.Bets.Count()

        let editBetAction = createEditBetAction context

        Assert.Throws<NotFoundException>(fun () ->
            editBetAction.AsyncExecute editingBets
            |> Async.RunSynchronously
            |> ignore)
        |> ignore
    }

[<Theory>]
[<MemberData(nameof (editingBetsWithMatchStatusNonEqualToPlaying))>]
let ``given a bet wich has an match non equal to pending when an action to edit them is executed then a EditingBetNotAllowedException is raised`` (editingBets: EditingBet []) =
    async {

        let context = createContext ()

        createExampleDataForEditBet (context)

        let editBetAction = createEditBetAction context

        Assert.Throws<EditingBetNotAllowedException>(fun () ->
            editBetAction.AsyncExecute editingBets
            |> Async.RunSynchronously
            |> ignore)
        |> ignore
    }
