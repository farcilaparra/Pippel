namespace Pippel.Data

open Pippel.Core

[<Interface>]
type IQueryRepository<'T when 'T: not struct> =

    /// Gets several items
    abstract AsyncFind: IQueryObject -> Async<obj seq>
    
    /// Gets several items with pagination
    abstract AsyncFind: IQueryObject * int * int -> Async<obj Page>
