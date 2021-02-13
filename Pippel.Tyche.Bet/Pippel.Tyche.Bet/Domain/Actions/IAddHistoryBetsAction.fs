namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IAddHistoryBetsAction =

    abstract AsyncExecute: HistoryBet seq -> Async<HistoryBet seq>
