namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries

type OpenedGroupsMatchesByGamblerQueryObject(gamblerID: Guid) =

    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            let now = DateTime.Now
            (query :?> IQueryable<MatchGroupGamblerViewDao>)
                .Where(fun x ->
                    x.GamblerID = gamblerID
                    && now >= x.StartDate
                    && now < x.EndDate)
                .OrderByDescending(fun x -> x.StartDate) :> IQueryable
