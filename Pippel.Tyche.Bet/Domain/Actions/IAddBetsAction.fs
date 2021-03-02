namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IAddBetsAction =

    abstract AsyncExecute : BetDomain seq -> Async<BetDomain seq>
