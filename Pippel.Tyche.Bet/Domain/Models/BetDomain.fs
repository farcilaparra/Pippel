namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Tyche.Bet.Type
open Pippel.Type

type BetPK =
    { PoolID: Uuid
      GamblerID: Uuid
      MatchID: Uuid }

type BetDomain =
    { ID: BetPK
      HomeTeamValue: Score
      AwayTeamValue: Score }
