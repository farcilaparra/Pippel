namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
type MatchGroupGamblerViewDto =
    { GroupBetID: Guid
      GamblerID: Guid
      GroupMatchName: string
      CurrentPoint: int
      CurrentPosition: int
      BeforePosition: int }
