module MatchDataHelper

open System
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

let createMatchesGroupsViewDaosToReturn () =
    [| { MatchGroupViewDao.GroupMatchId =
             Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
             |> Uuid.from
         HomeTeamName = "Colombia" |> NonEmptyString.from
         AwayTeamName = "Brazil" |> NonEmptyString.from
         MatchDate = DateTime.now }
       { MatchGroupViewDao.GroupMatchId =
             Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
             |> Uuid.from
         HomeTeamName = "Argentina" |> NonEmptyString.from
         AwayTeamName = "Perú" |> NonEmptyString.from
         MatchDate = DateTime.now } |]

let createOnPlayingMatchesViewDaosToReturn () =
    [| { OnPlayingMatchViewDao.MatchID =
             Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
             |> Uuid.from
         HomeTeamID =
             Guid("d557bea9-f5df-4115-b45a-ae240b8a19bc")
             |> Uuid.from
         AwayTeamID =
             Guid("7c63f1de-55d5-47d1-8831-4d2bdf3c0106")
             |> Uuid.from
         MatchDate = DateTime.now
         HomeTeamName = "Colombia" |> NonEmptyString.from
         AwayTeamName = "Brazil" |> NonEmptyString.from
         GroupMatchID =
             Guid("716cc6df-7ae0-4534-9d86-0e2438dfcfac")
             |> Uuid.from }
       { OnPlayingMatchViewDao.MatchID =
             Guid("044351a8-6bfa-4315-b7c7-874b45c95149")
             |> Uuid.from
         HomeTeamID =
             Guid("305e841b-cffc-4c62-9955-43d7021f0107")
             |> Uuid.from
         AwayTeamID =
             Guid("81488a27-5635-45e0-a223-4c76b1e1bcea")
             |> Uuid.from
         MatchDate = DateTime.now
         HomeTeamName = "Colombia" |> NonEmptyString.from
         AwayTeamName = "Brazil" |> NonEmptyString.from
         GroupMatchID =
             Guid("eee62b95-89a4-4d20-9b72-c83d7b6e5cba")
             |> Uuid.from } |]
