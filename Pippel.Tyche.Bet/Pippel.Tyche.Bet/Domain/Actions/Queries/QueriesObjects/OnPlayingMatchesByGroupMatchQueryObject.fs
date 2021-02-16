namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries

type OnPlayingMatchesByGroupMatchQueryObject(groupMatchID: Guid) =

    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            (query :?> IQueryable<OnPlayingMatchViewDao>)
                .Where(fun x -> x.GroupMatchID = groupMatchID)
                .OrderBy(fun x -> x.MatchDate) :> IQueryable
