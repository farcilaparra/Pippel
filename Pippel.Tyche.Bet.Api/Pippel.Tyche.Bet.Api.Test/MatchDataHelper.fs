module MatchDataHelper

open System
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

let createMasterPoolMatchesViewDaosToReturn () =
    [| { MasterPoolMatchViewDao.MasterPoolID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         MatchDate = DateTime.Now }
       { MasterPoolMatchViewDao.MasterPoolID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamName = "Argentina"
         AwayTeamName = "Perú"
         MatchDate = DateTime.Now } |]

let createOnPlayingMatchesViewDaosToReturn () =
    [| { OnPlayingMatchViewDao.MatchID = Guid("c6bc7971-1261-4bb8-9f57-b14013129315")
         HomeTeamID = Guid("d557bea9-f5df-4115-b45a-ae240b8a19bc")
         AwayTeamID = Guid("7c63f1de-55d5-47d1-8831-4d2bdf3c0106")
         MatchDate = DateTime.Now
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         MasterPoolID = Guid("716cc6df-7ae0-4534-9d86-0e2438dfcfac") }
       { OnPlayingMatchViewDao.MatchID = Guid("044351a8-6bfa-4315-b7c7-874b45c95149")
         HomeTeamID = Guid("305e841b-cffc-4c62-9955-43d7021f0107")
         AwayTeamID = Guid("81488a27-5635-45e0-a223-4c76b1e1bcea")
         MatchDate = DateTime.Now
         HomeTeamName = "Colombia"
         AwayTeamName = "Brazil"
         MasterPoolID = Guid("eee62b95-89a4-4d20-9b72-c83d7b6e5cba") } |]
