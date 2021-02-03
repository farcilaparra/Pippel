namespace Pippel.Data

open System.Linq
open Pippel.Core.IQueryable

type DynamicQueryParam =
    { Select: string option
      Where: string option
      GroupBy: string option
      OrderBy: string option }

type DynamicQueryObject(dynamicQueryParam: DynamicQueryParam) =

    /// Applies a contidition to a query
    let ApplyWhere (query: IQueryable) (dynamicQueryParam: DynamicQueryParam): IQueryable =
        match dynamicQueryParam.Where with
        | Some where -> query.Where where
        | None -> query


    /// Applies a order by criteria to a query
    let ApplyOrderBy (query: IQueryable) (dynamicQueryParam: DynamicQueryParam): IQueryable =
        match dynamicQueryParam.OrderBy with
        | Some orderBy -> query.OrderBy(orderBy) :> IQueryable
        | None -> query


    /// Applies a group by criteria to a query
    let ApplyGroupBy (query: IQueryable) (dynamicQueryParam: DynamicQueryParam): IQueryable =
        match dynamicQueryParam.GroupBy with
        | Some groupBy -> query.GroupBy groupBy
        | None -> query


    /// Applies a select criteria to a query
    let ApplySelect (query: IQueryable) (dynamicQueryParam: DynamicQueryParam): IQueryable =
        match dynamicQueryParam.Select with
        | Some select -> query.Select select
        | None -> query


    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            let mutable newQuery = query

            newQuery <- ApplyWhere newQuery dynamicQueryParam
            newQuery <- ApplyOrderBy newQuery dynamicQueryParam
            newQuery <- ApplyGroupBy newQuery dynamicQueryParam
            ApplySelect newQuery dynamicQueryParam
