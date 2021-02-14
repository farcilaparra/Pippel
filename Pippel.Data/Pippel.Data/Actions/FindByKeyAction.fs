namespace Pippel.Data.Actions

open Pippel.Core
open Pippel.Data

[<AbstractClass>]
type FindByKeyAction<'TData, 'TDomain when 'TData: not struct and 'TDomain: not struct>(repository: IRepository<'TData>,
                                                                                        mapper: IMapper<'TDomain, 'TData>) =

    /// Find an item by its primary key
    abstract AsyncExecute: obj [] -> Async<'TDomain>

    default this.AsyncExecute(key: obj []): Async<'TDomain> =
        async {
            let! dao = repository.AsyncFindByKey(key)
            return dao |> mapper.Map
        }
