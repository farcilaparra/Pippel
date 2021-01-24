namespace Pippel.Tax.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tax.Data.Models
open Pippel.Tax.Domain.Models
open Pippel.Tax.Data.Repositories

type AddVatsAction(vatRepository: IVatRepository, unitOfWork: IUnitOfWork, vatMapper: IMapper<Vat, VatDao>) =
    inherit AddAction<VatDao, Vat>(vatRepository, unitOfWork, vatMapper)
