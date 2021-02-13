namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IEditBetAction =
    
    abstract AsyncExecute: EditingBet seq -> Async<Bet seq>
