namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchesByGroupMatchQueryObject(groupMatchID: Guid) =

    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            (query :?> IQueryable<MatchGroupViewDao>)
                .Where(fun x -> x.GroupMatchId = groupMatchID)
                .OrderBy(fun x -> x.MatchDate) :> IQueryable
