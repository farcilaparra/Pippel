namespace Pippel.Data.EntityFrameworkCore

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Core
open Pippel.Data

type Repository<'TEntity when 'TEntity: not struct>(context: DbContext) =

    let asyncGetEntityByKey (ids: obj []) : Async<'TEntity> =
        async {
            let! item =
                context.Set<'TEntity>().FindAsync(ids).AsTask()
                |> Async.AwaitTask

            if isNull <| box item then
                NotFoundException($"The item with ids = ({String.Join(',', ids)}) doesn't exist")
                |> raise
            context.Entry(item).State <- EntityState.Detached

            return item
        }

    interface IRepository<'TEntity> with

        member this.AsyncFindByKey(ids: obj []) : Async<'TEntity> = asyncGetEntityByKey ids

        member this.AsyncFind<'TResult>(queryObject: IQueryObject) : Async<'TResult seq> =
            QueryRepository.asyncFind<'TEntity, 'TResult> context queryObject

        member this.AsyncFindWithPagination<'TResult>
            (queryObject: IQueryObject)
            (skip: int)
            (take: int)
            : Async<'TResult Page> =
            QueryRepository.asyncFindWithPagination<'TEntity, 'TResult> context queryObject skip take

        member this.AsyncAdd(items: 'TEntity seq) : Async<'TEntity seq> =
            async {
                let mutable addedItems : 'TEntity list = []

                for item in items do
                    let entry = context.Entry(item)

                    let ids =
                        entry
                            .Metadata
                            .FindPrimaryKey()
                            .Properties.Select(fun x -> entry.Property(x.Name).CurrentValue)
                            .ToArray()

                    let! itemToAdd =
                        context.Set<'TEntity>().FindAsync(ids).AsTask()
                        |> Async.AwaitTask

                    if box itemToAdd |> isNull |> not then
                        AlreadyExistException(
                            String.Format("The item with ids = ({0}) already exists", String.Join(",", ids))
                        )
                        |> raise

                    let! entityEntry =
                        (context.AddAsync(item)).AsTask()
                        |> Async.AwaitTask

                    addedItems <- [ (entityEntry.Entity :?> 'TEntity) ] @ addedItems

                return addedItems |> List.toSeq
            }

        member this.AsyncUpdate(items: 'TEntity seq) : Async<'TEntity seq> =
            async {
                let mutable updatedItems : 'TEntity list = []

                for item in items do
                    let entry = context.Entry(item)

                    let key =
                        (entry
                            .Metadata
                            .FindPrimaryKey()
                            .Properties.Select(fun x -> entry.Property(x.Name).CurrentValue)
                            .ToArray())

                    let! itemToUpdate = asyncGetEntityByKey key

                    context
                        .Entry(itemToUpdate)
                        .CurrentValues.SetValues(item)

                    let entityEntry = context.Update(itemToUpdate)

                    updatedItems <-
                        [ (entityEntry.Entity :?> 'TEntity) ]
                        @ updatedItems

                return updatedItems |> List.toSeq
            }

        member this.AsyncRemove(ids: obj [] seq) : Async<'TEntity seq> =
            async {
                let mutable removedItems : 'TEntity list = []

                for id in ids do
                    let! entity = asyncGetEntityByKey id
                    let entityEntry = context.Remove entity

                    removedItems <-
                        [ (entityEntry.Entity :?> 'TEntity) ]
                        @ removedItems

                return removedItems |> List.toSeq
            }
