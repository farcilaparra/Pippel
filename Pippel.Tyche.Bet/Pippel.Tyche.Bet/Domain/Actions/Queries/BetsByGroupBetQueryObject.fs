namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open System
open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries

type BetsByGroupBetQueryObject(groupBetID: Guid) =
    
    interface IQueryObject with

        member this.Query(sourceQuery: IQueryable): IQueryable =
            let typedSourceQuery = sourceQuery :?> IQueryable<BetPositionViewDao>
            query { for item in typedSourceQuery do
                    where (item.GroupBetID = groupBetID)
                    sortByDescending item.Point.HasValue
                    thenByDescending item.Point.Value
                    thenBy item.EnrollmentDate
                    select item } :> IQueryable
