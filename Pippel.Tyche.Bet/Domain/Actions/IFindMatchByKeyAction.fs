namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IFindMatchByKeyAction =

    abstract AsyncExecute : MatchPK -> Async<MatchDomain>
