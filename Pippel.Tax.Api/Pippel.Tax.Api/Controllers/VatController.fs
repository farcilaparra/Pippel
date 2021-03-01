namespace Pippel.Tax.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Data
open Pippel.Tax.Domain.Mappers
open Pippel.Tax.Domain.Models
open Pippel.Type
open Pippel.Core
open Pippel.Tax.Actions
open Pippel.Tax.Data.Repositories
open Pippel.Tax.View.Models
open Pippel.Tax.View.Mappers

[<ApiController>]
[<Route("[controller]")>]
type VatController
    (
        logger: ILogger<VatController>,
        vatRepository: IVatRepository,
        unitOfWork: IUnitOfWork,
        vatDomainMapper: VatDomainMapper,
        vatViewMapper: VatViewMapper
    ) =
    inherit ControllerBase()

    /// <summary>Converts an item to <c>VatDto</c> if is a <c>VatDao</c></summary>
    let convertItem (obj: obj) : obj =
        if obj :? Vat then
            (obj :?> Vat)
            |> (vatViewMapper :> IMapper<VatDto, Vat>)
                .MapToSource
            :> obj
        else
            obj

    [<HttpGet("{id}")>]
    member this.GetByKey(id: string) : Async<VatDto> =
        async {
            let findVatByKeyAction =
                FindVatByKeyAction(vatRepository, vatDomainMapper)

            let! vat = findVatByKeyAction.AsyncExecute [| (id |> Uuid.create) |]

            return
                vat
                |> (vatViewMapper :> IMapper<VatDto, Vat>)
                    .MapToSource
        }

    [<HttpGet>]
    member this.Get
        (skip: int)
        (take: int)
        (select: string)
        (where: string)
        (groupBy: string)
        (orderBy: string)
        : Async<Page<obj>> =
        async {
            let findVatsAction =
                FindVatsAction(vatRepository, vatDomainMapper)

            let! page =
                findVatsAction.AsyncExecute
                    { QueryObject.Skip = skip
                      Take = take
                      Select =
                          match isNull select with
                          | true -> None
                          | false -> Some select
                      Where =
                          match isNull where with
                          | true -> None
                          | false -> Some where
                      GroupBy =
                          match isNull groupBy with
                          | true -> None
                          | false -> Some groupBy
                      OrderBy =
                          match isNull orderBy with
                          | true -> None
                          | false -> Some orderBy }

            return
                { Page.CurrentPage = page.CurrentPage
                  PageCount = page.PageCount
                  PageSize = page.PageSize
                  ItemsCount = page.ItemsCount
                  GroupCount = page.GroupCount
                  Items = page.Items |> Seq.map (fun x -> convertItem x) }
        }

    [<HttpPost>]
    member this.Add(vats: VatDto seq) : Async<VatDto seq> =
        async {
            let addVatsAction =
                AddVatsAction(vatRepository, unitOfWork, vatDomainMapper)

            let! addedVats =
                addVatsAction.AsyncExecute(
                    vats
                    |> Seq.map
                        (fun x ->
                            (match isNull x.Id with
                             | true ->
                                 { x with
                                       Id = Guid.NewGuid().ToString() }
                             | false -> x)
                            |> (vatViewMapper :> IMapper<VatDto, Vat>)
                                .MapToTarget)
                )

            return
                addedVats
                |> Seq.map
                    (fun x ->
                        x
                        |> (vatViewMapper :> IMapper<VatDto, Vat>)
                            .MapToSource)
        }

    [<HttpPut>]
    member this.Update(vats: VatDto seq) : Async<VatDto seq> =
        async {
            let updateVatsAction =
                UpdateVatsAction(vatRepository, unitOfWork, vatDomainMapper)

            let! updatedVats =
                updateVatsAction.AsyncExecute(
                    vats
                    |> Seq.map
                        (fun x ->
                            x
                            |> (vatViewMapper :> IMapper<VatDto, Vat>)
                                .MapToTarget)
                )

            return
                updatedVats
                |> Seq.map
                    (fun x ->
                        x
                        |> (vatViewMapper :> IMapper<VatDto, Vat>)
                            .MapToSource)
        }

    [<HttpDelete>]
    member this.Remove(ids: string seq) : Async<VatDto seq> =
        async {
            let removeVatsAction =
                RemoveVatsAction(vatRepository, unitOfWork, vatDomainMapper)

            let! removedVats =
                removeVatsAction.AsyncExecute(
                    ids
                    |> Seq.map (fun x -> [| (x |> Uuid.create) :> obj |])
                )

            return
                removedVats
                |> Seq.map
                    (fun x ->
                        x
                        |> (vatViewMapper :> IMapper<VatDto, Vat>)
                            .MapToSource)
        }
