namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindBetsByGroupBetAction =

    abstract AsyncExecute: Uuid -> Async<BetPositionViewDao seq>
