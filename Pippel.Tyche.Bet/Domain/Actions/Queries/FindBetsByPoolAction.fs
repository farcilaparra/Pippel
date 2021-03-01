namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindBetsByPoolAction(repository: IQueryRepository<BetPositionViewDao>) =

    interface IFindBetsByPoolAction with

        member this.AsyncExecute(groupBetID: Uuid) : Async<BetPositionViewDao seq> =
            async { return! repository.AsyncFind<BetPositionViewDao>(BetsByPoolQueryObject(groupBetID)) }
