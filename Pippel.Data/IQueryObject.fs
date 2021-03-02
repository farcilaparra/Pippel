namespace Pippel.Data

open System.Linq

[<Interface>]
type IQueryObject =

    abstract Query : IQueryable -> IQueryable
