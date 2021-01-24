namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type GroupMatchDao =
    { ID: Guid
      Name: string
      StartDate: DateTime
      EndDate: DateTime }
            