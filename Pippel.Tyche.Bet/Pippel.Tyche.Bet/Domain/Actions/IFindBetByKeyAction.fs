namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

type IFindBetByKeyAction =

    abstract AsyncExecute : obj [] -> Async<Bet>
