namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IEditBetAction =

    abstract AsyncExecute : BetDomain seq -> Async<BetDomain seq>
