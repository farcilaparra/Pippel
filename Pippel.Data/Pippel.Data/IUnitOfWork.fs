namespace Pippel.Data

[<Interface>]
type IUnitOfWork =

    /// Commits the changes
    abstract SaveChanges: unit -> unit
