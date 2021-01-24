namespace Pippel.Tax.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tax.Data.Models
open Pippel.Type
open Pippel.Tax.Domain.Models
open Pippel.Tax.Data.Repositories

type RemoveVatsAction(vatRepository: IVatRepository, unitOfWork: IUnitOfWork, vatMapper: IMapper<Vat, VatDao>) =
    inherit RemoveAction<VatDao, Vat>(vatRepository, unitOfWork, vatMapper)

    override this.AsyncExecute(key: obj [] seq): Async<Vat seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
