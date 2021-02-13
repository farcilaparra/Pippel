namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

type IFindMatchByKeyAction =

    abstract AsyncExecute: obj [] -> Async<Match>
