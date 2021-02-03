namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type Bet =
    { ID: Uuid
      GroupBetID: Uuid
      GamblerID: Uuid
      MatchID: Uuid
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt
      LastPosition: PositiveInt }