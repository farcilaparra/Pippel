namespace Pippel.Tax.Actions

open Pippel.Core
open Pippel.Tax.Data.Models
open Pippel.Type
open Pippel.Tax.Domain.Models
open Pippel.Tax.Data.Repositories
open Pippel.Data.Actions

type FindVatByKeyAction(vatRepository: IVatRepository, vatMapper: IMapper<Vat, VatDao>) =
    inherit FindByKeyAction<VatDao, Vat>(vatRepository, vatMapper)

    override this.AsyncExecute(key: obj []) : Async<Vat> =
        base.AsyncExecute(
            key
            |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)
        )
