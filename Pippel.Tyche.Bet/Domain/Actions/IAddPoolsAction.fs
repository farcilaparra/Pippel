namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IAddPoolsAction =

    abstract AsyncExecute: PoolDomain seq -> Async<PoolDomain seq>
