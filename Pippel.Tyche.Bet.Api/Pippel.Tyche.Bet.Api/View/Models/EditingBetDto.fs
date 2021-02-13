namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
type EditingBetDto =
    { GroupBetID: Guid
      MatchID: Guid
      GamblerID: Guid
      HomeTeamValue: int
      AwayTeamValue: int }
