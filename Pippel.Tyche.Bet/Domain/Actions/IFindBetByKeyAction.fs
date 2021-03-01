namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IFindBetByKeyAction =

    abstract AsyncExecute : BetPK -> Async<BetDomain>
