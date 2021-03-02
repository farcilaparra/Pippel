namespace Pippel.Tyche.Bet.Api.Data.Models

open Pippel.Type

type EditingBetDto =
    { PoolID: Uuid
      MatchID: Uuid
      GamblerID: Uuid
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt }
