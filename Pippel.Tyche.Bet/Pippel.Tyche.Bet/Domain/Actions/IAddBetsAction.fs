namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IAddBetsAction =

    abstract AsyncExecute: Bet seq -> Async<Bet seq>
