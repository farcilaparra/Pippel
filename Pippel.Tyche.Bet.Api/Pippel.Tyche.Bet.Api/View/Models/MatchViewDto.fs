namespace Pippel.Tyche.Bet.Api.Data.Models

open System
            
type MatchViewDto =
    { MatchID: Guid
      RoundMatchID: Guid
      GroupMatchID: Guid
      GroupBetID: Guid
      MatchStatus: int
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeTeamName: string
      AwayTeamName: string
      Point: int Nullable }
