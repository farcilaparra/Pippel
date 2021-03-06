namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type IFindMatchesByPoolAction =

    abstract AsyncExecute : Uuid -> Async<MatchViewDao seq>
