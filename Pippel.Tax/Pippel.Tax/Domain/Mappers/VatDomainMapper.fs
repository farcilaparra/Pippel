namespace Pippel.Tax.Domain.Mappers

open Pippel.Core
open Pippel.Tax.Domain.Models
open Pippel.Tax.Data.Models
open Pippel.Type

type VatDomainMapper() =
    interface IMapper<Vat, VatDao> with

        /// <summary>Maps from <c>Vat</c> to <c>VatDao</c></summary>
        member this.MapToTarget(vat: Vat): VatDao =
            { VatDao.ID = vat.ID |> Uuid.toGuid
              Name = vat.Name |> NonEmptyString.value
              Percentage = vat.Percentage |> Percentage.value }

        /// <summary>Maps from <c>VatDao</c> to <c>Vat</c></summary>
        member this.MapToSource(vatDao: VatDao): Vat =
            { Vat.ID = vatDao.ID |> Uuid.createFromGuid
              Name = vatDao.Name |> NonEmptyString.create
              Percentage = vatDao.Percentage |> Percentage.create }
