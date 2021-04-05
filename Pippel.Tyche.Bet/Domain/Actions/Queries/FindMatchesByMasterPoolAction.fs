namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindMatchesByMasterPoolAction(repository: IQueryRepository<MasterPoolMatchViewDao>) =

    interface IFindMatchesByMasterPoolAction with

        member this.AsyncExecute(groupMatchID: Uuid) =
            async { return! repository.AsyncFind<MasterPoolMatchViewDao>(MatchesByMasterPoolQueryObject(groupMatchID)) }
