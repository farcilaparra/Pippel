namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindOpenedMasterPoolsByGamblerAction(repository: IQueryRepository<PoolReviewViewDao>) =

    interface IFindOpenedMasterPoolsByGamblerAction with

        member this.AsyncExecute(gamblerID: Uuid) : Async<PoolReviewViewDao seq> =
            async { return! repository.AsyncFind<PoolReviewViewDao>(OpenedMasterPoolsByGamblerQueryObject(gamblerID)) }
