module MatchDataHelper

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

let createMasterPoolMatchesViewDaosToReturn () =
    [| { MasterPoolMatchViewDao.MasterPoolID =
             "c6bc7971-1261-4bb8-9f57-b14013129315"
             |> Uuid.fromString
         HomeTeamName = "Colombia" |> NotEmptyString.from
         AwayTeamName = "Brazil" |> NotEmptyString.from
         MatchDate = DateTime.now }
       { MasterPoolMatchViewDao.MasterPoolID =
             "c6bc7971-1261-4bb8-9f57-b14013129315"
             |> Uuid.fromString
         HomeTeamName = "Argentina" |> NotEmptyString.from
         AwayTeamName = "Perú" |> NotEmptyString.from
         MatchDate = DateTime.now } |]

let createOnPlayingMatchesViewDaosToReturn () =
    [| { OnPlayingMatchViewDao.MatchID =
             "c6bc7971-1261-4bb8-9f57-b14013129315"
             |> Uuid.fromString
         HomeTeamID =
             "d557bea9-f5df-4115-b45a-ae240b8a19bc"
             |> Uuid.fromString
         AwayTeamID =
             "7c63f1de-55d5-47d1-8831-4d2bdf3c0106"
             |> Uuid.fromString
         MatchDate = DateTime.now
         HomeTeamName = "Colombia" |> NotEmptyString.from
         AwayTeamName = "Brazil" |> NotEmptyString.from
         MasterPoolID =
             "716cc6df-7ae0-4534-9d86-0e2438dfcfac"
             |> Uuid.fromString }
       { OnPlayingMatchViewDao.MatchID =
             "044351a8-6bfa-4315-b7c7-874b45c95149"
             |> Uuid.fromString
         HomeTeamID =
             "305e841b-cffc-4c62-9955-43d7021f0107"
             |> Uuid.fromString
         AwayTeamID =
             "81488a27-5635-45e0-a223-4c76b1e1bcea"
             |> Uuid.fromString
         MatchDate = DateTime.now
         HomeTeamName = "Colombia" |> NotEmptyString.from
         AwayTeamName = "Brazil" |> NotEmptyString.from
         MasterPoolID =
             "eee62b95-89a4-4d20-9b72-c83d7b6e5cba"
             |> Uuid.fromString } |]
