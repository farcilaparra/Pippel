namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type Bet =
    { ID: Uuid
      GroupBetID: Uuid
      GamblerID: Uuid
      MatchID: Uuid
      HomeTeamValue: PositiveInt32
      AwayTeamValue: PositiveInt32
      LastPosition: PositiveInt32 }