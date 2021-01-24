namespace Pippel.Tax.View.Mappers

open Pippel.Core
open Pippel.Tax.Domain.Models
open Pippel.Tax.View.Models
open Pippel.Type

type VatViewMapper() =
    interface IMapper<VatDto, Vat> with
    
        /// <summary>Maps from <c>VatDto</c> to <c>Vat</c></summary>
        member this.MapToTarget(vatDto: VatDto): Vat =
            { Vat.ID = vatDto.Id |> Uuid.create
              Name = vatDto.Name |> NonEmptyString.create
              Percentage = vatDto.Percentage |> Percentage.create }
        
        /// <summary>Maps from <c>Vat</c> to <c>VatDto</c></summary>
        member this.MapToSource(vat: Vat): VatDto =
            { VatDto.Id = vat.ID |> Uuid.value
              Name = vat.Name |> NonEmptyString.value
              Percentage = vat.Percentage |> Percentage.value }
