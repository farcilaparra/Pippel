namespace Pippel.Data

open Pippel.Core

[<Interface>]
type IQueryRepository<'TSource when 'TSource: not struct> =

    abstract AsyncFind<'TResult> : IQueryObject -> Async<'TResult seq>

    abstract AsyncFindWithPagination<'TResult> : IQueryObject -> int -> int -> Async<'TResult Page>
