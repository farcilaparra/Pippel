namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

type IFindGroupBetByKeyAction =

    abstract AsyncExecute: obj [] -> Async<GroupBet>
