namespace Pippel.Tyche.Bet.Actions.Queries

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchesByGroupBetQueryObject(groupBetID: Guid) =

    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            (query :?> IQueryable<MatchViewDao>)
                .Where(fun x -> x.GroupBetID = groupBetID)
                .OrderBy(fun x -> x.MatchDate) :> IQueryable
