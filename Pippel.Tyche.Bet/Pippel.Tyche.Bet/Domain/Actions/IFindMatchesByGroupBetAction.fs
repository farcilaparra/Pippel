namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type IFindMatchesByGroupBetAction =

    abstract AsyncExecute: Uuid -> Async<MatchViewDao seq>
