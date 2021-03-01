namespace Pippel.Data

open Pippel.Core

module Actions =

    let private convertItemIfDataModelType<'TDomain, 'TData when 'TDomain: not struct and 'TData: not struct>
        (mapToDomainFunc: 'TData -> 'TDomain)
        (obj: obj)
        : obj =
        match obj with
        | :? 'TData -> (obj :?> 'TData) |> mapToDomainFunc :> obj
        | _ -> obj

    let asyncFindByKey (ids: obj []) (mapToDomainFunc) (repository: IRepository<'TData>) : Async<'TDomain> =
        async {
            let! item = repository.AsyncFindByKey ids
            return item |> mapToDomainFunc
        }

    let asyncFind<'TDomain, 'TData, 'TResult when 'TDomain: not struct and 'TData: not struct>
        (queryObject: IQueryObject)
        (mapToDomainFunc: 'TData -> 'TDomain)
        (repository: IRepository<'TData>)
        : Async<'TResult seq> =
        async {
            let! items = repository.AsyncFind queryObject

            return
                items
                |> Seq.map (fun item -> (item |> convertItemIfDataModelType mapToDomainFunc) :?> 'TResult)
        }

    let asyncFindWithPagination<'TDomain, 'TData, 'TResult when 'TDomain: not struct and 'TData: not struct>
        (queryObject: IQueryObject)
        (skip: int)
        (take: int)
        (mapToDomainFunc: 'TData -> 'TDomain)
        (repository: IRepository<'TData>)
        : Async<'TResult Page> =
        async {
            let! page = repository.AsyncFindWithPagination queryObject skip take

            let items =
                page.Items
                |> Seq.map (fun x -> (x |> convertItemIfDataModelType mapToDomainFunc) :?> 'TResult)

            return
                { page with
                      Items = items }
        }

    let asyncAdd
        (items: 'TDomain seq)
        (mapFromDomainFunc)
        (mapToDomainFunc)
        (repository: IRepository<'TData>)
        : Async<'TDomain seq> =
        async {
            let! items = repository.AsyncAdd(items |> Seq.map (fun x -> x |> mapFromDomainFunc))
            return items |> Seq.map (fun x -> x |> mapToDomainFunc)
        }

    let asyncUpdate
        (items: 'TDomain seq)
        (mapFromDomainFunc)
        (mapToDomainFunc)
        (repository: IRepository<'TData>)
        : Async<'TDomain seq> =
        async {
            let! items = repository.AsyncUpdate(items |> Seq.map (fun x -> x |> mapFromDomainFunc))
            return items |> Seq.map (fun x -> x |> mapToDomainFunc)
        }

    let asyncRemove (ids: obj [] seq) (mapToDomainFunc) (repository: IRepository<'TData>) : Async<'TDomain seq> =
        async {
            let! items = repository.AsyncRemove ids
            return items |> Seq.map (fun x -> x |> mapToDomainFunc)
        }
