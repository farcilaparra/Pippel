namespace Pippel.Data.Actions

open Pippel.Core
open Pippel.Data

[<AbstractClass>]
type RemoveAction<'TData, 'TDomain when 'TData: not struct and 'TDomain: not struct>(repository: IRepository<'TData>,
                                                                                     unitOfWork: IUnitOfWork,
                                                                                     mapper: IMapper<'TDomain, 'TData>) =

    /// Removes several items
    abstract AsyncExecute: obj [] seq -> Async<'TDomain seq>

    default this.AsyncExecute(key: obj [] seq): Async<'TDomain seq> =
        async {
            let! daos = repository.AsyncRemove(key)
            unitOfWork.SaveChanges()
            return daos |> Seq.map (fun x -> x |> mapper.MapToSource)
        }
