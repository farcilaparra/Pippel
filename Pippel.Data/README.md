# Base project to manage the data persistence in F#

## IRepository

It's an interface with the basic functions to persist data.

```f#
[<Interface>]
type IRepository<'T when 'T: not struct> =

    abstract AsyncFindByKey: obj [] -> Async<'T>

    abstract AsyncFind: IQueryObject -> Async<obj Page>

    abstract AsyncAdd: 'T seq -> Async<'T seq>

    abstract AsyncUpdate: 'T seq -> Async<'T seq>

    abstract AsyncRemove: obj [] seq -> Async<'T seq>
```

## Repository

It's an implementation of `IRepository` that uses Entity Framework Core. It has five functions:

* **AsyncFindByKey**: Finds an item by its primary key.
* **AsyncFind**: Finds several items by the given criteria.
* **AsyncAdd**: Marks serveral items for persist.
* **AsyncUpdate**: Marks serveral items for update.
* **AsyncRemove**: Marks several items for remove.

## IUnitOfWork

It's an interface to manage the changes to the persistence.

```f#
[<Interface>]
type IUnitOfWork =

    abstract SaveChanges: unit -> unit
```

## UnitOfWork

It's an implementatio of `IUnitOfWork` that uses Entity Framework Core.

## AddAction

It executes the action to mark an item for persist.

## FindAction

It executes the action to find several items by any criteria.

## FindByKeyAction

It executes the action to find an item by ids primary key.

## RemoveAction

It executes the action to mark an item for remove.

## UpdateAction

It executes the action to mark several items for update.