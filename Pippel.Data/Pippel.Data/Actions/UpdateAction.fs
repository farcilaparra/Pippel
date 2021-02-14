namespace Pippel.Data.Actions

open Pippel.Core
open Pippel.Data

[<AbstractClass>]
type UpdateAction<'TData, 'TDomain when 'TData: not struct and 'TDomain: not struct>(repository: IRepository<'TData>,
                                                                                     unitOfWork: IUnitOfWork,
                                                                                     mapper: IMapper<'TDomain, 'TData>) =

    /// Updates several items
    abstract AsyncExecute: 'TDomain seq -> Async<'TDomain seq>

    default this.AsyncExecute(vats: 'TDomain seq): Async<'TDomain seq> =
        async {
            let! daos = repository.AsyncUpdate(vats |> Seq.map (fun x -> x |> mapper.Map))
            unitOfWork.SaveChanges()
            return daos |> Seq.map (fun x -> x |> mapper.Map)
        }
