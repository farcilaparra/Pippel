namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type OpenedMasterPoolsByGamblerQueryObject(gamblerID: Uuid, filter: NotEmptyString option) =

    interface IQueryObject with

        member this.Query(query: IQueryable) : IQueryable =
            let mutable newQuery = query
            let now = DateTime.Now
            let gamblerID = gamblerID |> Uuid.value

            newQuery <-
                (newQuery :?> IQueryable<PoolReviewViewDao>)
                    .Where(fun element ->
                        element.GamblerID = gamblerID
                        && now >= element.StartDate
                        && now < element.EndDate)
                :> IQueryable

            newQuery <-
                match filter with
                | Some it ->
                    match it |> NotEmptyString.value with
                    | value ->
                        (newQuery :?> IQueryable<PoolReviewViewDao>)
                            .Where(fun element -> element.MasterPoolName.Contains(value))
                        :> IQueryable
                | None -> newQuery

            newQuery
