namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
            
[<CLIMutable>]
type MatchGroupViewDao =
    { GroupMatchId: Guid
      HomeTeamName: String
      AwayTeamName: String
      MatchDate: DateTime }
