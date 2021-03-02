namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type BetsByPoolQueryObject(groupBetID: Uuid) =

    interface IQueryObject with

        member this.Query(sourceQuery: IQueryable) : IQueryable =
            let typedSourceQuery =
                sourceQuery :?> IQueryable<BetPositionViewDao>

            query {
                for item in typedSourceQuery do
                    where (item.PoolID = groupBetID)
                    sortByDescending item.Point.IsSome
                    thenByDescending item.Point.Value
                    thenBy item.EnrollmentDate
                    select item
            }
            :> IQueryable
