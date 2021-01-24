namespace Pippel.Tax.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tax.Data.Models
open Pippel.Tax.Data.Repositories
open Pippel.Tax.Domain.Models

type FindVatsAction(vatRepository: IVatRepository, vatMapper: IMapper<Vat, VatDao>) =
    inherit FindAction<VatDao, Vat>(vatRepository, vatMapper)
