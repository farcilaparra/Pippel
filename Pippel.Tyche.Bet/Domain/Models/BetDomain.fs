namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type BetPK =
    { PoolID: Uuid
      GamblerID: Uuid
      MatchID: Uuid }

type BetDomain =
    { ID: BetPK
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt }
