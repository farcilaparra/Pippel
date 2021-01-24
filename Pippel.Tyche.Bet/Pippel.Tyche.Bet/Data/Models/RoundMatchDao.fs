namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type RoundMatchDao =
    { ID: Guid
      GroupMatchID: Guid
      Name: string }
            