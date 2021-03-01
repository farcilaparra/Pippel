namespace Pippel.Type

open System
open System.ComponentModel
open System.Globalization
open System.Linq
open System.Linq.Expressions
open System.Text.Json
open System.Text.Json.Serialization
open Microsoft.EntityFrameworkCore.Storage.ValueConversion
open Microsoft.FSharp.Linq.RuntimeHelpers
open Validation

type Barcode = private Barcode of string

module Barcode =

    [<Literal>]
    let private regularExpressionForBarcode = @"^(\d{8}|\d{12,14})$"

    let (|NotChekSum|_|) (value: string) =
        let paddedCode = value.PadLeft(14, '0')

        let sum =
            paddedCode
                .Select(fun c i ->
                    Int32.Parse(c.ToString())
                    * (match i % 2 = 0 with
                       | true -> 3
                       | false -> 1))
                .Sum()

        (sum % 10) = 0 |> not |> ifTrueThen NotChekSum

    let tryFrom element =
        match element with
        | Null
        | NotMatches regularExpressionForBarcode
        | NotChekSum -> None
        | _ -> Barcode element |> Some

    let from element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (Barcode element) = func element

    let value element = apply id element

    let toString (element: Barcode) = (element |> value).ToString()

    module internal Model =

        let fromModel (element: Barcode) = element |> value

        let toModel (element: string) = element |> from

        let tryToModel element = tryFrom element

    module internal EntityFrameworkCore =

        let internal toModelExpression =
            <@ Func<string, Barcode>(from) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<string, Barcode>>>

        let internal fromModelExpression =
            <@ Func<Barcode, string>(value) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Barcode, string>>>

        let toOption element =
            match isNull element with
            | true -> None
            | false -> Some(element |> from)

        let internal toOptionExpression =
            <@ Func<string, Barcode option>(toOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<string, Barcode option>>>

        let fromOption element =
            match element with
            | Some y -> y |> value
            | None -> Unchecked.defaultof<string>

        let internal fromOptionExpression =
            <@ Func<Barcode option, string>(fromOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Barcode option, string>>>

    type BarcodeTypeConverter() =
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

    type BarcodeJsonConverter() =
        inherit JsonConverter<Barcode>()

        override this.Read
            (
                reader: byref<Utf8JsonReader>,
                typeToConvert: Type,
                options: JsonSerializerOptions
            ) : Barcode =
            Model.toModel (reader.GetString())

        override this.Write(writer: Utf8JsonWriter, value: Barcode, options: JsonSerializerOptions) =
            writer.WriteStringValue(Model.fromModel value)

    type BarcodeValueConverter() =
        inherit ValueConverter<Barcode, string>(EntityFrameworkCore.fromModelExpression,
                                                EntityFrameworkCore.toModelExpression)

    type BarcodeOptionValueConverter() =
        inherit ValueConverter<Barcode option, string>(EntityFrameworkCore.fromOptionExpression,
                                                       EntityFrameworkCore.toOptionExpression)

    let initTypeConverter () =
        TypeDescriptor.AddAttributes(typeof<Barcode>, TypeConverterAttribute typeof<BarcodeTypeConverter>)
        |> ignore
