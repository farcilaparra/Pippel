namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindMatchesByGroupMatchAction =

    abstract AsyncExecute: Uuid -> Async<MatchGroupViewDao seq>
