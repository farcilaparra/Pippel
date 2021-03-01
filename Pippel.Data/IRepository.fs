namespace Pippel.Data

open Pippel.Core

[<Interface>]
type IRepository<'TEntity when 'TEntity: not struct> =

    abstract AsyncFindByKey : obj [] -> Async<'TEntity>

    abstract AsyncFind<'TResult> : IQueryObject -> Async<'TResult seq>

    abstract AsyncFindWithPagination<'TResult> : IQueryObject -> int -> int -> Async<'TResult Page>

    abstract AsyncAdd : 'TEntity seq -> Async<'TEntity seq>

    abstract AsyncUpdate : 'TEntity seq -> Async<'TEntity seq>

    abstract AsyncRemove : obj [] seq -> Async<'TEntity seq>
