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

type DateTime = private DateTime of System.DateTime

module DateTime =

    [<Literal>]
    let private regularExpressionForDateTime =
        @"^(?:[1-9]\d{3}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1\d|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[1-9]\d(?:0[48]|[2468][048]|[13579][26])|(?:[2468][048]|[13579][26])00)-02-29)T(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d(?:\.\d{1,9})?(?:Z|[+-][01]\d:[0-5]\d)$"

    let tryFrom element =
        match element with
        | Null
        | NotMatches regularExpressionForDateTime -> None
        | _ ->
            match DateTime.TryParse(element) with
            | true, d -> DateTime d |> Some
            | _ -> None

    let from element = DateTime element

    let fromString element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (DateTime element) = func element

    let value element = apply id element

    let now = System.DateTime.Now |> from

    let toString (element: DateTime) = (element |> value).ToString("o")

    let toStringWithFormat (format: string) (element: DateTime) = (element |> value).ToString(format)

    let toUniversalTime (element: DateTime) =
        (element |> value).ToUniversalTime() |> from

    module internal Model =

        let fromModel (element: DateTime) =
            (element |> value).ToUniversalTime().ToString("o")

        let toModel (element: string) = element |> fromString

        let tryToModel element = tryFrom element

    module internal EntityFrameworkCore =

        let toModelExpression =
            <@ Func<System.DateTime, DateTime>(from) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<System.DateTime, DateTime>>>

        let fromModelExpression =
            <@ Func<DateTime, System.DateTime>(value) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<DateTime, System.DateTime>>>

        let toOption (element: System.DateTime Nullable) =
            match element.HasValue with
            | false -> None
            | true -> Some(element.Value |> from)

        let toOptionExpression =
            <@ Func<System.DateTime Nullable, DateTime option>(toOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<System.DateTime Nullable, DateTime option>>>

        let fromOption element =
            match element with
            | Some y -> y |> value |> Nullable
            | None -> Unchecked.defaultof<System.DateTime Nullable>

        let fromOptionExpression =
            <@ Func<DateTime option, System.DateTime Nullable>(fromOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<DateTime option, System.DateTime Nullable>>>

    type DateTimeTypeConverter() =
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

    type DateTimeJsonConverter() =
        inherit JsonConverter<DateTime>()

        override this.Read
            (
                reader: byref<Utf8JsonReader>,
                typeToConvert: Type,
                options: JsonSerializerOptions
            ) : DateTime =
            Model.toModel (reader.GetString())

        override this.Write(writer: Utf8JsonWriter, value: DateTime, options: JsonSerializerOptions) =
            writer.WriteStringValue(Model.fromModel value)

    type DateTimeValueConverter() =
        inherit ValueConverter<DateTime, System.DateTime>(EntityFrameworkCore.fromModelExpression,
                                                          EntityFrameworkCore.toModelExpression)

    type DateTimeOptionValueConverter() =
        inherit ValueConverter<DateTime option, System.DateTime Nullable>(EntityFrameworkCore.fromOptionExpression,
                                                                          EntityFrameworkCore.toOptionExpression)

    let initTypeConverter () =
        TypeDescriptor.AddAttributes(typeof<DateTime>, TypeConverterAttribute typeof<DateTimeTypeConverter>)
        |> ignore
