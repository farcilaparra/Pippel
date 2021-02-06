namespace Pippel.Data.EntityFrameworkCore

open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Core
open Pippel.Data

module QueryRepository =

    let asyncFind<'T when 'T: not struct> (context: DbContext) (queryObject: IQueryObject): Async<obj seq> =
        async {
            let mutable query =
                context.Set<'T>().AsNoTracking() :> IQueryable

            query <- queryObject.Query query

            let! items =
                (query :?> IQueryable<obj>).ToListAsync()
                |> Async.AwaitTask

            return items :> seq<obj>
        }


    let asyncFindWithPagination<'T when 'T: not struct> (context: DbContext)
                                                        (queryObject: IQueryObject)
                                                        (skip: int)
                                                        (take: int)
                                                        : Async<obj Page> =
        async {
            let mutable query =
                context.Set<'T>().AsNoTracking() :> IQueryable

            let! itemsCount =
                (query :?> IQueryable<'T>).LongCountAsync()
                |> Async.AwaitTask

            query <- queryObject.Query query

            let! groupCount =
                (query :?> IQueryable<obj>).CountAsync()
                |> Async.AwaitTask

            query <- (query :?> IQueryable<obj>).Skip(skip).Take(take) :> IQueryable

            let! pageCount =
                (query :?> IQueryable<obj>).CountAsync()
                |> Async.AwaitTask

            let! items =
                (query :?> IQueryable<obj>).ToListAsync()
                |> Async.AwaitTask

            return
                { Page.CurrentPage = skip / take
                  PageSize = take
                  PageCount = pageCount
                  GroupCount = groupCount
                  ItemsCount = itemsCount
                  Items = items }
        }


type QueryRepository<'T when 'T: not struct>(context: DbContext) =

    interface IQueryRepository<'T> with

        member this.AsyncFind(queryObject: IQueryObject): Async<obj seq> =
            QueryRepository.asyncFind<'T> context queryObject

        member this.AsyncFind(queryObject: IQueryObject, skip: int, take: int): Async<obj Page> =
            QueryRepository.asyncFindWithPagination<'T> context queryObject skip take
