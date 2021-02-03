namespace Pippel.Data.EntityFrameworkCore

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Core
open Pippel.Data

type Repository<'T when 'T: not struct>(context: DbContext) =

    /// Gets an item by its primary key
    let AsyncGetEntityByKey (ids: obj []): Async<'T> =
        async {
            let! item =
                context.Set<'T>().FindAsync(ids).AsTask()
                |> Async.AwaitTask

            if isNull (box item) then
                NotFoundException(String.Format("The item with ids = ({0}) doesn't exist", String.Join(",", ids)))
                |> raise

            context.Entry(item).State <- EntityState.Detached

            return item
        }


    interface IRepository<'T> with

        member this.AsyncFindByKey([<ParamArray>] ids: obj []): Async<'T> =
            async { return! AsyncGetEntityByKey(ids) }


        member this.AsyncFind(queryObject: IQueryObject): Async<obj seq> =
            async {
                let mutable query =
                    context.Set<'T>().AsNoTracking() :> IQueryable

                query <- queryObject.Query query

                let! items =
                    (query :?> IQueryable<obj>).ToListAsync()
                    |> Async.AwaitTask

                return items :> seq<obj>
            }

        member this.AsyncFind(queryObject: IQueryObject, skip: int, take: int): Async<obj Page> =
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

        member this.AsyncAdd(items: 'T seq): Async<'T seq> =
            async {
                let mutable addedItems: 'T list = []

                for item in items do
                    let entry = context.Entry(item)

                    let ids =
                        entry
                            .Metadata
                            .FindPrimaryKey()
                            .Properties.Select(fun x -> entry.Property(x.Name).CurrentValue)
                            .ToArray()

                    let! itemToAdd =
                        context.Set<'T>().FindAsync(ids).AsTask()
                        |> Async.AwaitTask

                    if not (isNull (box itemToAdd)) then
                        AlreadyExistException
                            (String.Format("The item with ids = ({0}) already exists", String.Join(",", ids)))
                        |> raise

                    let! entityEntry =
                        (context.AddAsync(item)).AsTask()
                        |> Async.AwaitTask

                    addedItems <- [ (entityEntry.Entity :?> 'T) ] @ addedItems

                return addedItems |> List.toSeq
            }

        member this.AsyncUpdate(items: 'T seq): Async<'T seq> =
            async {
                let mutable updatedItems: 'T list = []

                for item in items do
                    let entry = context.Entry(item)

                    let! itemToUpdate =
                        AsyncGetEntityByKey
                            (entry
                                .Metadata
                                .FindPrimaryKey()
                                .Properties.Select(fun x -> entry.Property(x.Name).CurrentValue)
                                .ToArray())

                    context
                        .Entry(itemToUpdate)
                        .CurrentValues.SetValues(item)

                    let entityEntry = context.Update(itemToUpdate)

                    updatedItems <- [ (entityEntry.Entity :?> 'T) ] @ updatedItems

                return updatedItems |> List.toSeq
            }

        member this.AsyncRemove(ids: obj [] seq): Async<'T seq> =
            async {
                let mutable removedItems: 'T list = []

                for id in ids do
                    let! entity = AsyncGetEntityByKey(id)
                    let entityEntry = context.Remove entity

                    removedItems <- [ (entityEntry.Entity :?> 'T) ] @ removedItems

                return removedItems |> List.toSeq
            }
