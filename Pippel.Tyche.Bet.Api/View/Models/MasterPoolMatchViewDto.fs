namespace Pippel.Tyche.Bet.Data.Models

open System

type MasterPoolMatchViewDto =
    { MasterPoolID: Guid
      HomeTeamName: string
      AwayTeamName: string
      MatchDate: DateTime }
