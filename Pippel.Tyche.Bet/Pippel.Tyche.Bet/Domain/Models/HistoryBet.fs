namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type HistoryBet =
    { ID: Uuid
      BetID: Uuid
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt
      CreationDate: DateTime }
