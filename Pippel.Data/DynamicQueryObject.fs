namespace Pippel.Data

open System.Linq
open Pippel.Core.IQueryable

type DynamicQueryParam =
    { Select: string option
      Where: (string * obj []) option
      GroupBy: string option
      OrderBy: string option }

type DynamicQueryObject(dynamicQueryParam: DynamicQueryParam) =

    let ApplyWhere (query: IQueryable) (dynamicQueryParam: DynamicQueryParam) : IQueryable =
        match dynamicQueryParam.Where with
        | Some where ->
            match where with
            | (where, args) -> query.Where(where, args)
        | None -> query

    let ApplyOrderBy (query: IQueryable) (dynamicQueryParam: DynamicQueryParam) : IQueryable =
        match dynamicQueryParam.OrderBy with
        | Some orderBy -> query.OrderBy(orderBy) :> IQueryable
        | None -> query

    let ApplyGroupBy (query: IQueryable) (dynamicQueryParam: DynamicQueryParam) : IQueryable =
        match dynamicQueryParam.GroupBy with
        | Some groupBy -> query.GroupBy groupBy
        | None -> query

    let ApplySelect (query: IQueryable) (dynamicQueryParam: DynamicQueryParam) : IQueryable =
        match dynamicQueryParam.Select with
        | Some select -> query.Select select
        | None -> query

    interface IQueryObject with

        member this.Query(query: IQueryable) : IQueryable =
            let mutable newQuery = query

            newQuery <- ApplyWhere newQuery dynamicQueryParam
            newQuery <- ApplyOrderBy newQuery dynamicQueryParam
            newQuery <- ApplyGroupBy newQuery dynamicQueryParam
            ApplySelect newQuery dynamicQueryParam
