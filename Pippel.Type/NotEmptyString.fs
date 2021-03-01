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

type NotEmptyString = private NotEmptyString of string

module NotEmptyString =

    let tryFrom element =
        match element with
        | Null
        | WhiteSpaces -> None
        | _ -> NotEmptyString element |> Some

    let from element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (NotEmptyString element) = func element

    let value element = apply id element

    let toString (element: NotEmptyString) = (element |> value).ToString()

    module internal Model =

        let fromModel (element: NotEmptyString) = element |> value

        let toModel (element: string) = element |> from

        let tryToModel element = tryFrom element

    module internal EntityFrameworkCore =

        let internal toModelExpression =
            <@ Func<string, NotEmptyString>(from) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<string, NotEmptyString>>>

        let internal fromModelExpression =
            <@ Func<NotEmptyString, string>(value) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<NotEmptyString, string>>>

        let toOption element =
            match isNull element with
            | true -> None
            | false -> Some(element |> from)

        let internal toOptionExpression =
            <@ Func<string, NotEmptyString option>(toOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<string, NotEmptyString option>>>

        let fromOption element =
            match element with
            | Some y -> y |> value
            | None -> Unchecked.defaultof<string>

        let internal fromOptionExpression =
            <@ Func<NotEmptyString option, string>(fromOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<NotEmptyString option, string>>>

    type NotEmptyStringTypeConverter() =
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

    type NotEmptyStringJsonConverter() =
        inherit JsonConverter<NotEmptyString>()

        override this.Read
            (
                reader: byref<Utf8JsonReader>,
                typeToConvert: Type,
                options: JsonSerializerOptions
            ) : NotEmptyString =
            Model.toModel (reader.GetString())

        override this.Write(writer: Utf8JsonWriter, value: NotEmptyString, options: JsonSerializerOptions) =
            writer.WriteStringValue(Model.fromModel value)

    type NotEmptyStringValueConverter() =
        inherit ValueConverter<NotEmptyString, string>(EntityFrameworkCore.fromModelExpression,
                                                       EntityFrameworkCore.toModelExpression)

    type NotEmptyStringOptionValueConverter() =
        inherit ValueConverter<NotEmptyString option, string>(EntityFrameworkCore.fromOptionExpression,
                                                              EntityFrameworkCore.toOptionExpression)

    let initTypeConverter () =
        TypeDescriptor.AddAttributes(typeof<NotEmptyString>, TypeConverterAttribute typeof<NotEmptyStringTypeConverter>)
        |> ignore
