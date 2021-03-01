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

type PositiveInt = private PositiveInt of int

module PositiveInt =

    let tryFrom element =
        match element with
        | Less 0 -> None
        | _ -> PositiveInt element |> Some

    let from element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (PositiveInt element) = func element

    let value element = apply id element

    let toString (element: PositiveInt) = (element |> value).ToString()

    module internal Model =

        let fromModel (element: PositiveInt) = element |> value |> string

        let toModel (element: string) = element |> int |> from

        let tryToModel (element: String) =
            match Int32.TryParse(element) with
            | true, d -> tryFrom d
            | _ -> None

    module internal EntityFrameworkCore =

        let internal toModelExpression =
            <@ Func<int, PositiveInt>(from) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<int, PositiveInt>>>

        let internal fromModelExpression =
            <@ Func<PositiveInt, int>(value) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<PositiveInt, int>>>

        let toOption (element: int Nullable) =
            match element.HasValue with
            | false -> None
            | true -> Some(element.Value |> from)

        let internal toOptionExpression =
            <@ Func<int Nullable, PositiveInt option>(toOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<int Nullable, PositiveInt option>>>

        let fromOption element =
            match element with
            | Some y -> y |> value |> Nullable
            | None -> Unchecked.defaultof<int Nullable>

        let internal fromOptionExpression =
            <@ Func<PositiveInt option, int Nullable>(fromOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<PositiveInt option, int Nullable>>>

    type PositiveIntTypeConverter() =
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

    type PositiveIntJsonConverter() =
        inherit JsonConverter<PositiveInt>()

        override this.Read
            (
                reader: byref<Utf8JsonReader>,
                typeToConvert: Type,
                options: JsonSerializerOptions
            ) : PositiveInt =
            Model.toModel (reader.GetString())

        override this.Write(writer: Utf8JsonWriter, value: PositiveInt, options: JsonSerializerOptions) =
            writer.WriteStringValue(Model.fromModel value)

    type PositiveIntValueConverter() =
        inherit ValueConverter<PositiveInt, int>(EntityFrameworkCore.fromModelExpression,
                                                 EntityFrameworkCore.toModelExpression)

    type PositiveIntOptionValueConverter() =
        inherit ValueConverter<PositiveInt option, int Nullable>(EntityFrameworkCore.fromOptionExpression,
                                                                 EntityFrameworkCore.toOptionExpression)

    let initTypeConverter () =
        TypeDescriptor.AddAttributes(typeof<PositiveInt>, TypeConverterAttribute typeof<PositiveIntTypeConverter>)
        |> ignore
