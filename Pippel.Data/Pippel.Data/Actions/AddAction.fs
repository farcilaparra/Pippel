namespace Pippel.Data.Actions

open Pippel.Core
open Pippel.Data

type AddAction<'TData, 'TDomain when 'TData: not struct and 'TDomain: not struct>(repository: IRepository<'TData>,
                                                                                  unitOfWork: IUnitOfWork,
                                                                                  mapper: IMapper<'TDomain, 'TData>) =

    /// Ads several items
    abstract AsyncExecute: 'TDomain seq -> Async<'TDomain seq>

    default this.AsyncExecute(vats: 'TDomain seq): Async<'TDomain seq> =
        async {
            let! daos = repository.AsyncAdd(vats |> Seq.map (fun x -> x |> mapper.MapToTarget))
            unitOfWork.SaveChanges()
            return daos |> Seq.map (fun x -> x |> mapper.MapToSource)
        }