namespace Pippel.Data.EntityFrameworkCore

open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Core
open Pippel.Data

module QueryRepository =

    let asyncFind<'TEntity, 'TResult when 'TEntity: not struct>
        (context: DbContext)
        (queryObject: IQueryObject)
        : Async<'TResult seq> =
        async {
            let mutable query =
                context.Set<'TEntity>().AsNoTracking() :> IQueryable

            query <- queryObject.Query query

            let! items =
                (query :?> IQueryable<'TResult>).ToListAsync()
                |> Async.AwaitTask

            return items :> 'TResult seq
        }


    let asyncFindWithPagination<'TEntity, 'TResult when 'TEntity: not struct>
        (context: DbContext)
        (queryObject: IQueryObject)
        (skip: int)
        (take: int)
        : Async<'TResult Page> =
        async {
            let mutable query =
                context.Set<'TEntity>().AsNoTracking() :> IQueryable

            query <- queryObject.Query query

            let! itemsCount =
                (query :?> IQueryable<'TResult>).LongCountAsync()
                |> Async.AwaitTask

            query <-
                (query :?> IQueryable<'TResult>)
                    .Skip(skip)
                    .Take(take)
                :> IQueryable

            let! pageCount =
                (query :?> IQueryable<'TResult>).CountAsync()
                |> Async.AwaitTask

            let! items =
                (query :?> IQueryable<'TResult>).ToListAsync()
                |> Async.AwaitTask

            return
                { Page.CurrentPage = skip / take
                  PageSize = take
                  PageCount = pageCount
                  ItemsCount = itemsCount
                  Items = items }
        }

type QueryRepository<'TEntity when 'TEntity: not struct>(context: DbContext) =

    interface IQueryRepository<'TEntity> with

        member this.AsyncFind<'TResult>(queryObject: IQueryObject) : Async<'TResult seq> =
            QueryRepository.asyncFind<'TEntity, 'TResult> context queryObject

        member this.AsyncFindWithPagination<'TResult>
            (queryObject: IQueryObject)
            (skip: int)
            (take: int)
            : Async<'TResult Page> =
            QueryRepository.asyncFindWithPagination<'TEntity, 'TResult> context queryObject skip take
