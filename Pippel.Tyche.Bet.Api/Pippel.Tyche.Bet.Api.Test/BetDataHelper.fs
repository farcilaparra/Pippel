module BetDataHelper

open System
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Type
open Pippel.Type

let createBetsToReturn () =
    [| { BetDomain.ID =
             { PoolID = Uuid.From "718d7467-383f-4094-81c4-5b104d7969aa"
               GamblerID = Uuid.From "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
               MatchID = Uuid.From "107d3c40-faff-4980-8041-8763b4f43d42" }
         HomeTeamValue = Score.From 0
         AwayTeamValue = Score.From 1 }
       { BetDomain.ID =
             { PoolID = Uuid.From "43c2483f-9b3e-48b8-978c-99e07d2f29d2"
               GamblerID = Uuid.From "c32a2d10-88d7-4c60-bc18-01d8f0c3110e"
               MatchID = Uuid.From "107d3c40-faff-4980-8041-8763b4f43d42" }
         HomeTeamValue = Score.From 2
         AwayTeamValue = Score.From 1 } |]

let createEditingBetsDtosToSend () =
    [| { EditingBetDto.MatchID = Guid("718d7467-383f-4094-81c4-5b104d7969aa")
         GamblerID = Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
         PoolID = Guid("107d3c40-faff-4980-8041-8763b4f43d42")
         HomeTeamValue = 0
         AwayTeamValue = 1 }
       { EditingBetDto.MatchID = Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
         GamblerID = Guid("c32a2d10-88d7-4c60-bc18-01d8f0c3110e")
         PoolID = Guid("107d3c40-faff-4980-8041-8763b4f43d42")
         HomeTeamValue = 2
         AwayTeamValue = 1 } |]

let createPoolsReviewToReturn () =
    [| { PoolReviewViewDao.PoolID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         MasterPoolID = Guid("697b3701-640e-4cef-a20a-12ce6e8d7e3b")
         GamblerID = Guid("718d7467-383f-4094-81c4-5b104d7969aa")
         MasterPoolName = "Eliminatorias al Mundial Qatar 2024 23/24 febrero"
         StartDate = DateTime.Now
         EndDate = DateTime.Now
         CurrentPoint = 10 |> Nullable
         CurrentPosition = 3 |> Nullable
         BeforePosition = 4 |> Nullable }
       { PoolReviewViewDao.PoolID = Guid("67c53e8f-c4a6-4a1a-8b5b-55c902660395")
         MasterPoolID = Guid("f608cdd7-2694-4f46-b5ce-a2db030789da")
         GamblerID = Guid("43c2483f-9b3e-48b8-978c-99e07d2f29d2")
         MasterPoolName = "Eliminatorias al Mundial Qatar 2024 02/03 mayo"
         StartDate = DateTime.Now
         EndDate = DateTime.Now
         CurrentPoint = 15 |> Nullable
         CurrentPosition = 8 |> Nullable
         BeforePosition = 4 |> Nullable } |]

let createMatchesViewDaosToReturn () =
    [| { MatchViewDao.MatchID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         RoundID = Guid("673e3cdf-77ed-4be8-80fa-5c3e4eed977a")
         MasterPoolID = Guid("f306c573-755c-45f7-bf7f-d715b572d51e")
         PoolID = Guid("19c6abc4-bc86-4b49-91c8-50c53dd8b3ad")
         MatchStatus = MatchStatus.Playing
         HomeTeamID = Guid("2144d3f2-ee6e-47ab-827c-6fc08df6af2a")
         AwayTeamID = Guid("e3a2551a-70ea-428f-bdb1-7a75f55f96da")
         MatchDate = DateTime.Now
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         Point = 20 |> Nullable }
       { MatchViewDao.MatchID = Guid("efb920a9-6b50-46eb-8fb3-2044b9a7de55")
         RoundID = Guid("d0dd2b75-cf37-4239-8dc3-a1179b250ab8")
         MasterPoolID = Guid("626a4dcf-b775-45aa-a82d-057de0cd4c36")
         PoolID = Guid("4fd172fd-b2f8-4518-b54b-bd50599ea672")
         MatchStatus = MatchStatus.Playing
         HomeTeamID = Guid("337824e9-c2bd-4680-b4ac-f5cac3c64f20")
         AwayTeamID = Guid("12a1cf96-c3f7-4ce3-9c22-0a8883889348")
         MatchDate = DateTime.Now
         HomeTeamName = "Argentina"
         AwayTeamName = "Uruguay"
         Point = 10 |> Nullable } |]

let createBetsPositionsViewDaosToReturn () =
    [| { BetPositionViewDao.PoolID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         GamblerID = Guid("673e3cdf-77ed-4be8-80fa-5c3e4eed977a")
         EnrollmentDate = DateTime.Now
         Point = Nullable<int>()
         CurrentPosition = Nullable<int>()
         BeforePosition = Nullable<int>() }
       { BetPositionViewDao.PoolID = Guid("9b74072c-6df0-4ade-9655-4cff4dc35faa")
         GamblerID = Guid("d29b78b4-8fff-406e-875a-480de5f5ca94")
         EnrollmentDate = DateTime.Now
         Point = 15 |> Nullable
         CurrentPosition = 1 |> Nullable
         BeforePosition = Nullable<int>() } |]
