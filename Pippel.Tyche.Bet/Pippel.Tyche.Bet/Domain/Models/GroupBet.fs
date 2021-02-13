namespace Pippel.Tyche.Bet.Domain.Models

open System
open Pippel.Type

type GroupBet =
    { ID: Uuid
      GroupMatchID: Uuid
      OwnerGamblerID: Uuid
      CreationDate: DateTime }
