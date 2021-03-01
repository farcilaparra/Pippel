namespace Pippel.Type

open System
open System.ComponentModel
open System.Globalization
open System.Linq.Expressions
open System.Text.Json
open System.Text.Json.Serialization
open Microsoft.EntityFrameworkCore.Storage.ValueConversion
open Microsoft.FSharp.Linq.RuntimeHelpers
open Validation

type Percentage = private Percentage of float

module Percentage =

    let tryFrom element =
        match element with
        | NotRange 0.0 1.0 -> None
        | _ -> Percentage element |> Some

    let from element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (Percentage element) = func element

    let value element = apply id element

    let toString (element: Percentage) = (element |> value).ToString()

    module internal Model =

        let fromModel (element: Percentage) = element |> value |> string

        let toModel (element: string) = element |> float |> from

        let tryToModel (element: String) =
            match System.Double.TryParse(element) with
            | true, d -> tryFrom d
            | _ -> None

    module internal EntityFrameworkCore =

        let internal toModelExpression =
            <@ Func<float, Percentage>(from) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<float, Percentage>>>

        let internal fromModelExpression =
            <@ Func<Percentage, float>(value) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Percentage, float>>>

        let toOption (element: float Nullable) =
            match element.HasValue with
            | false -> None
            | true -> Some(element.Value |> from)

        let internal toOptionExpression =
            <@ Func<float Nullable, Percentage option>(toOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<float Nullable, Percentage option>>>

        let fromOption element =
            match element with
            | Some y -> y |> value |> Nullable
            | None -> Unchecked.defaultof<float Nullable>

        let internal fromOptionExpression =
            <@ Func<Percentage option, float Nullable>(fromOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Percentage option, float Nullable>>>

    type PercentageTypeConverter() =
        inherit TypeConverter()

        override this.CanConvertFrom(context: ITypeDescriptorContext, sourceType: Type) =
            match sourceType = typeof<string> with
            | true -> true
            | false -> base.CanConvertFrom(context, sourceType)

        override this.ConvertFrom(context: ITypeDescriptorContext, culture: CultureInfo, value: obj) =
            match value :? string with
            | true ->
                match Model.tryToModel (value :?> string) with
                | Some d -> d :> obj
                | None -> base.ConvertFrom(context, culture, value)
            | false -> base.ConvertFrom(context, culture, value)

    type PercentageJsonConverter() =
        inherit JsonConverter<Percentage>()

        override this.Read
            (
                reader: byref<Utf8JsonReader>,
                typeToConvert: Type,
                options: JsonSerializerOptions
            ) : Percentage =
            Model.toModel (reader.GetString())

        override this.Write(writer: Utf8JsonWriter, value: Percentage, options: JsonSerializerOptions) =
            writer.WriteStringValue(Model.fromModel value)

    type PercentageValueConverter() =
        inherit ValueConverter<Percentage, float>(EntityFrameworkCore.fromModelExpression,
                                                  EntityFrameworkCore.toModelExpression)

    type PercentageOptionValueConverter() =
        inherit ValueConverter<Percentage option, float Nullable>(EntityFrameworkCore.fromOptionExpression,
                                                                  EntityFrameworkCore.toOptionExpression)

    let initTypeConverter () =
        TypeDescriptor.AddAttributes(typeof<Percentage>, TypeConverterAttribute typeof<PercentageTypeConverter>)
        |> ignore
