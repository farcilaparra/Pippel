namespace Pippel.Tyche.Bet.Data.Repositories.Queries

open Pippel.Data

[<Interface>]
type IQueryRepositoryFactory =
    
    abstract Get<'T when 'T: not struct> : unit -> IQueryRepository<'T>
