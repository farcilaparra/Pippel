module BetDataHelper

open System
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

let createBetsToReturn () =
    [| { BetDomain.GroupBetID =
             Guid("718d7467-383f-4094-81c4-5b104d7969aa")
             |> Uuid.from
         GamblerID =
             Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
             |> Uuid.from
         MatchID =
             Guid("107d3c40-faff-4980-8041-8763b4f43d42")
             |> Uuid.from
         HomeTeamValue = 0 |> PositiveInt.from
         AwayTeamValue = 1 |> PositiveInt.from }
       { BetDomain.GroupBetID =
             Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
             |> Uuid.from
         GamblerID =
             Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
             |> Uuid.from
         MatchID =
             Guid("107d3c40-faff-4980-8041-8763b4f43d42")
             |> Uuid.from
         HomeTeamValue = 2 |> PositiveInt.from
         AwayTeamValue = 1 |> PositiveInt.from } |]

let createEditingBetsDtosToSend () =
    [| { EditingBetDto.MatchID =
             Guid("718d7467-383f-4094-81c4-5b104d7969aa")
             |> Uuid.from
         GamblerID =
             Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
             |> Uuid.from
         GroupBetID =
             Guid("107d3c40-faff-4980-8041-8763b4f43d42")
             |> Uuid.from
         HomeTeamValue = 0 |> PositiveInt.from
         AwayTeamValue = 1 |> PositiveInt.from }
       { EditingBetDto.MatchID =
             Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
             |> Uuid.from
         GamblerID =
             Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
             |> Uuid.from
         GroupBetID =
             Guid("107d3c40-faff-4980-8041-8763b4f43d42")
             |> Uuid.from
         HomeTeamValue = 2 |> PositiveInt.from
         AwayTeamValue = 1 |> PositiveInt.from } |]

let createMatchesToReturn () =
    [| { MatchGroupGamblerViewDao.GroupBetID =
             Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
             |> Uuid.from
         GroupMatchID =
             Guid("697b3701-640e-4cef-a20a-12ce6e8d7e3b")
             |> Uuid.from
         GamblerID =
             Guid("718d7467-383f-4094-81c4-5b104d7969aa")
             |> Uuid.from
         GroupMatchName =
             "Eliminatorias al Mundial Qatar 2024 23/24 febrero"
             |> NonEmptyString.from
         StartDate = DateTime.now
         EndDate = DateTime.now
         CurrentPoint = 10 |> PositiveInt.from
         CurrentPosition = 3 |> PositiveInt.from
         BeforePosition = 4 |> PositiveInt.from }
       { MatchGroupGamblerViewDao.GroupBetID =
             Guid("67c53e8f-c4a6-4a1a-8b5b-55c902660395")
             |> Uuid.from
         GroupMatchID =
             Guid("f608cdd7-2694-4f46-b5ce-a2db030789da")
             |> Uuid.from
         GamblerID =
             Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
             |> Uuid.from
         GroupMatchName =
             "Eliminatorias al Mundial Qatar 2024 02/03 mayo"
             |> NonEmptyString.from
         StartDate = DateTime.now
         EndDate = DateTime.now
         CurrentPoint = 15 |> PositiveInt.from
         CurrentPosition = 8 |> PositiveInt.from
         BeforePosition = 4 |> PositiveInt.from } |]

let createMatchesViewDaosToReturn () =
    [| { MatchViewDao.MatchID =
             Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
             |> Uuid.from
         RoundMatchID =
             Guid("673e3cdf-77ed-4be8-80fa-5c3e4eed977a")
             |> Uuid.from
         GroupMatchID =
             Guid("f306c573-755c-45f7-bf7f-d715b572d51e")
             |> Uuid.from
         GroupBetID =
             Guid("19c6abc4-bc86-4b49-91c8-50c53dd8b3ad")
             |> Uuid.from
         MatchStatus = MatchStatus.Playing
         HomeTeamID =
             Guid("2144d3f2-ee6e-47ab-827c-6fc08df6af2a")
             |> Uuid.from
         AwayTeamID =
             Guid("e3a2551a-70ea-428f-bdb1-7a75f55f96da")
             |> Uuid.from
         MatchDate = DateTime.now
         HomeTeamName = "Colombia" |> NonEmptyString.from
         AwayTeamName = "Brazil" |> NonEmptyString.from
         Point = Some(20 |> PositiveInt.from) }
       { MatchViewDao.MatchID =
             Guid("efb920a9-6b50-46eb-8fb3-2044b9a7de55")
             |> Uuid.from
         RoundMatchID =
             Guid("d0dd2b75-cf37-4239-8dc3-a1179b250ab8")
             |> Uuid.from
         GroupMatchID =
             Guid("626a4dcf-b775-45aa-a82d-057de0cd4c36")
             |> Uuid.from
         GroupBetID =
             Guid("4fd172fd-b2f8-4518-b54b-bd50599ea672")
             |> Uuid.from
         MatchStatus = MatchStatus.Playing
         HomeTeamID =
             Guid("337824e9-c2bd-4680-b4ac-f5cac3c64f20")
             |> Uuid.from
         AwayTeamID =
             Guid("12a1cf96-c3f7-4ce3-9c22-0a8883889348")
             |> Uuid.from
         MatchDate = DateTime.now
         HomeTeamName = "Argentina" |> NonEmptyString.from
         AwayTeamName = "Uruguay" |> NonEmptyString.from
         Point = Some(10 |> PositiveInt.from) } |]

let createBetsPositionsViewDaosToReturn () =
    [| { BetPositionViewDao.GroupBetID =
             Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
             |> Uuid.from
         GamblerID =
             Guid("673e3cdf-77ed-4be8-80fa-5c3e4eed977a")
             |> Uuid.from
         EnrollmentDate = DateTime.now
         Point = None
         CurrentPosition = None
         BeforePosition = None }
       { BetPositionViewDao.GroupBetID =
             Guid("9b74072c-6df0-4ade-9655-4cff4dc35faa")
             |> Uuid.from
         GamblerID =
             Guid("d29b78b4-8fff-406e-875a-480de5f5ca94")
             |> Uuid.from
         EnrollmentDate = DateTime.now
         Point = Some(15 |> PositiveInt.from)
         CurrentPosition = Some(1 |> PositiveInt.from)
         BeforePosition = None } |]
