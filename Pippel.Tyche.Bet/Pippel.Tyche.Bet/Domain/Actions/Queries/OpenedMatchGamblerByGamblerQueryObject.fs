namespace Pippel.Tyche.Bet.Actions.Queries

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries

type OpenedMatchGamblerByGamblerQueryObject(gamblerID: Guid) =

    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            let now = DateTime.Now
            (query :?> IQueryable<MatchGamblerViewDao>)
                .Where(fun x ->
                    x.GamblerID = gamblerID
                    && now >= x.StartDate
                    && now < x.EndDate)
                .OrderByDescending(fun x -> x.StartDate) :> IQueryable
