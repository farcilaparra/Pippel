namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindOpenedGroupsMatchesByGamblerAction =

    abstract AsyncExecute: Uuid -> Async<MatchGamblerViewDao seq>
