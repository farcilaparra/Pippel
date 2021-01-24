namespace Pippel.Data

open Pippel.Core

type QueryObject =
    { Skip: int
      Take: int
      Select: string option
      Where: string option
      GroupBy: string option
      OrderBy: string option }

[<Interface>]
type IRepository<'T when 'T: not struct> =

    /// Gets a item for its primary key
    abstract AsyncFindByKey: obj [] -> Async<'T>

    /// Gets several items
    abstract AsyncFind: QueryObject -> Async<obj Page>

    /// Adds several items
    abstract AsyncAdd: 'T seq -> Async<'T seq>

    /// Updates several items
    abstract AsyncUpdate: 'T seq -> Async<'T seq>

    /// Removes several vats
    abstract AsyncRemove: obj [] seq -> Async<'T seq>
