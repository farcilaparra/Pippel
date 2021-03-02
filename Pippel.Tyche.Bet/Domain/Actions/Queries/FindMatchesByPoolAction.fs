namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindMatchesByPoolAction(repository: IQueryRepository<MatchViewDao>) =

    interface IFindMatchesByPoolAction with

        member this.AsyncExecute(groupBetID: Uuid) : Async<MatchViewDao seq> =
            async { return! repository.AsyncFind<MatchViewDao>(MatchesByPoolQueryObject(groupBetID)) }
