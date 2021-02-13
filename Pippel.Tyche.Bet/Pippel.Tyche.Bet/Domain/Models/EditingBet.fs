namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type EditingBet =
    { MatchID: Uuid
      GamblerID: Uuid
      GroupBetID: Uuid
      HomeValue: PositiveInt
      AwayValue: PositiveInt }
