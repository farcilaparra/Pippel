namespace Pippel.Data

open System
open System.Linq
open System.Linq.Expressions
open Pippel.Core


type OrderCriteriaType =
    | Ascending
    | Descending


type OrderCriteria<'T when 'T: not struct>(orderCriteriaPredicate: Expression<Func<'T, obj>>, orderCriteriaType: OrderCriteriaType) =
    
    let orderCriteriaPredicate = orderCriteriaPredicate
    
    let orderCriteriaType = orderCriteriaType
    
    member this.Apply(query: IQueryable) =
        match query.Expression.Type = typeof<IOrderedQueryable<'T>> with
        | true ->
            match orderCriteriaType with
            | OrderCriteriaType.Ascending ->
                (query :?> IOrderedQueryable<'T>)
                    .ThenBy(orderCriteriaPredicate)
            | OrderCriteriaType.Descending ->
                (query :?> IOrderedQueryable<'T>)
                    .ThenByDescending(orderCriteriaPredicate)
        | false ->
            match orderCriteriaType with
            | OrderCriteriaType.Ascending ->
                (query :?> IQueryable<'T>)
                    .OrderBy(orderCriteriaPredicate)
            | OrderCriteriaType.Descending ->
                (query :?> IQueryable<'T>)
                    .OrderByDescending(orderCriteriaPredicate)


type ExpressionQueryObject<'T when 'T: not struct> private (wherePredicate: Expression<Func<'T, bool>> option,
                                                            ordersCriterias: OrderCriteria<'T> seq option) =

    let mutable wherePredicate = wherePredicate

    let mutable ordersCriterias: OrderCriteria<'T> seq option = ordersCriterias


    let ApplyOrdersBy (query: IQueryable) (ordersCriterias: OrderCriteria<'T> seq) =
        let mutable newQuery = query

        for orderCriteria in ordersCriterias do
            newQuery <- orderCriteria.Apply newQuery
            
        newQuery


    new(wherePredicate: Expression<Func<'T, bool>>) = ExpressionQueryObject(Some wherePredicate, None)


    new(ordersCriterias: OrderCriteria<'T> seq) = ExpressionQueryObject(None, Some ordersCriterias)


    new(wherePredicate: Expression<Func<'T, bool>>, ordersCriterias: OrderCriteria<'T> seq) =
        ExpressionQueryObject(Some wherePredicate, Some ordersCriterias)


    interface IQueryObject with

        member this.Query(query: IQueryable): IQueryable =
            let mutable newQuery =
                match wherePredicate with
                | Some whereExpression -> (query :?> IQueryable<'T>).Where(whereExpression) :> IQueryable
                | None -> query

            match ordersCriterias with
            | Some ordersCriterias -> ApplyOrdersBy newQuery ordersCriterias
            | None -> newQuery
