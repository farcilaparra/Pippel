namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindBetsByPoolAction =

    abstract AsyncExecute : Uuid -> Async<BetPositionViewDao seq>
