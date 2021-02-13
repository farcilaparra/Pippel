namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IFindGroupBetGamblerByKeyAction =

    abstract AsyncExecute: obj [] -> Async<GroupBetGambler>
