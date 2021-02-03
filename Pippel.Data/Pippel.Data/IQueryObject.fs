namespace Pippel.Data

open System.Linq

[<Interface>]
type IQueryObject =

    /// Builds a query
    abstract Query: IQueryable -> IQueryable
