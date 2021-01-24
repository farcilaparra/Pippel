namespace Pippel.Core

open System.Linq
open System.Linq.Dynamic.Core

module IQueryable =

    type IQueryable with

        /// Selects by a text expression
        member this.Select(select: string) =
            DynamicQueryableExtensions.Select(this, select)
        
        /// Filters by a text expression
        member this.Where(where: string) =
            DynamicQueryableExtensions.Where(this, where)

        /// Orders by a text expression
        member this.OrderBy(orderBy: string) =
            DynamicQueryableExtensions.OrderBy(this, orderBy)

        /// Groups by a text expression
        member this.GroupBy(groupBy: string) =
            DynamicQueryableExtensions.GroupBy(this, groupBy)
