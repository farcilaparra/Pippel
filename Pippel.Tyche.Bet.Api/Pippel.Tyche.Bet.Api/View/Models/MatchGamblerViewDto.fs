namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
type MatchGamblerViewDto =
    { GroupBetID: Guid
      GamblerID: Guid
      GroupMatchName: string
      CurrentPoint: int
      CurrentPosition: int
      BeforePosition: int }
