namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IUpdateBetsAction =

    abstract AsyncExecute : BetDomain seq -> Async<BetDomain seq>
