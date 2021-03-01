namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type OpenedMasterPoolsByGamblerQueryObject(gamblerID: Uuid) =

    interface IQueryObject with

        member this.Query(query: IQueryable) : IQueryable =
            let now = DateTime.now

            (query :?> IQueryable<PoolReviewViewDao>)
                .Where(fun x ->
                    x.GamblerID = gamblerID
                    && now >= x.StartDate
                    && now < x.EndDate)
                .OrderByDescending(fun x -> x.StartDate)
            :> IQueryable
