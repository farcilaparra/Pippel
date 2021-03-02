# Base project to manage data persistence in F#

## `IQueryObject`

It's a interface to filter a `IQueryable`.

```f#
[<Interface>]
type IQueryObject =

    abstract Query: IQueryable -> IQueryable
```

## `DynamicQueryObject`

It's an implementation of `IQueryObject` that let filter an `IQueryable` by string criteria.

## `IRepository`

It's an interface with the basic functions to persist data.

```f#
[<Interface>]
type IRepository<'TEntity when 'TEntity: not struct> =

    abstract AsyncFindByKey : obj [] -> Async<'TEntity>

    abstract AsyncFind<'TResult> : IQueryObject -> Async<'TResult seq>

    abstract AsyncFindWithPagination<'TResult> : IQueryObject -> int -> int -> Async<'TResult Page>

    abstract AsyncAdd : 'TEntity seq -> Async<'TEntity seq>

    abstract AsyncUpdate : 'TEntity seq -> Async<'TEntity seq>

    abstract AsyncRemove : obj [] seq -> Async<'TEntity seq>
```

## `Repository`

It's an implementation of `IRepository` that uses Entity Framework Core.

### `AsyncFindByKey`

Finds an item by its primary key.

### `AsyncFind`

Finds several items by any criteria.

### `AsyncAdd`

Marks several items for persist.

### `AsyncUpdate`

Marks several items for update.

### `AsyncRemove`

Marks several items for remove.

## `IQueryRepository`

It's an interface to query data.

```f#
[<Interface>]
type IQueryRepository<'TSource when 'TSource: not struct> =

    abstract AsyncFind<'TResult> : IQueryObject -> Async<'TResult seq>

    abstract AsyncFindWithPagination<'TResult> : IQueryObject -> int -> int -> Async<'TResult Page>
```

## `QueryRepository`

It's an implementation of `IQueryRepository` that uses Entity Framework Core.

### `AsyncFind`

Finds several items by any criteria.

## `IUnitOfWork`

It's an interface to manage the changes to the persistence.

```f#
[<Interface>]
type IUnitOfWork =

    abstract SaveChanges: unit -> unit
```

## `UnitOfWork`

It's an implementatio of `IUnitOfWork` that uses Entity Framework Core.