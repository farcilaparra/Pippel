namespace Pippel.Data.Actions

open Pippel.Core
open Pippel.Data

type FindAction<'TData, 'TDomain when 'TData: not struct and 'TDomain: not struct>(repository: IRepository<'TData>,
                                                                                   mapper: IMapper<'TDomain, 'TData>) =

    /// Converts the item to 'TDomain if it is of type 'TData
    let convertItemIfDataModel (obj: obj): obj =
        if obj :? 'TData
        then (obj :?> 'TData) |> mapper.MapToSource :> obj
        else obj

    /// Find several items
    abstract AsyncExecute: IQueryObject -> Async<obj seq>

    default this.AsyncExecute(queryObject: IQueryObject): Async<obj seq> =
        async {
            let! items = repository.AsyncFind(queryObject)
            return items
        }
