namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
[<CLIMutable>]
type GroupMatchDto =
    { ID: Guid
      Name: string
      StartDate: DateTime
      EndDate: DateTime }
