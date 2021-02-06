namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
type MatchGamblerViewDto =
    { GroupBetID: Guid
      OwnerGamblerID: Guid
      Name: string }
