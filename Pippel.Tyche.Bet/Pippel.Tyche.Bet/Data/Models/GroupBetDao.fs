namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type GroupBetDao =
    { ID: Guid
      GroupMatchID: Guid
      OwnerGamblerID: Guid
      CreationDate: DateTime }
