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
    abstract AsyncExecute: QueryObject -> Async<obj Page>

    default this.AsyncExecute(queryObject: QueryObject): Async<obj Page> =
        async {
            let! page = repository.AsyncFind queryObject

            return { Page.Items = page.Items |> Seq.map (fun x -> x |> convertItemIfDataModel)
                     CurrentPage = page.CurrentPage
                     ItemsCount = page.ItemsCount
                     PageCount = page.PageCount
                     GroupCount = page.GroupCount
                     PageSize = page.PageSize }
        }
