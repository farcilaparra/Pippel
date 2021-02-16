namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindOpenedGroupsMatchesByGamblerAction =

    abstract AsyncExecute: Uuid -> Async<MatchGroupGamblerViewDao seq>
