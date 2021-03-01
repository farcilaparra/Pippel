namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindMatchesByMasterPoolAction =

    abstract AsyncExecute : Uuid -> Async<MasterPoolMatchViewDao seq>
