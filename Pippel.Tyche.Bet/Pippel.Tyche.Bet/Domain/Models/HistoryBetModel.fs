namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type HistoryBet =
    { ID: Uuid
      BetID: Uuid
      HomeTeamValue: PositiveInt32
      AwayTeamValue: PositiveInt32
      CreationDate: DateTime }
