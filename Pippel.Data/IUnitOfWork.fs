namespace Pippel.Data

[<Interface>]
type IUnitOfWork =

    abstract SaveChanges : unit -> unit
