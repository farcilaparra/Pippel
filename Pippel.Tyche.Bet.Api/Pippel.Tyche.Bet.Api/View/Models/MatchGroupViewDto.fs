namespace Pippel.Tyche.Bet.Data.Models

open System
            
[<CLIMutable>]
type MatchGroupViewDto =
    { GroupMatchId: Guid
      HomeTeamName: String
      AwayTeamName: String
      MatchDate: DateTime }
