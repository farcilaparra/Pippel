namespace Pippel.Type

open System
open System.ComponentModel
open System.Globalization
open System.Linq.Expressions
open System.Runtime.CompilerServices
open System.Text.Json
open System.Text.Json.Serialization
open Microsoft.EntityFrameworkCore.Storage.ValueConversion
open Microsoft.FSharp.Linq.RuntimeHelpers
open Validation

type Uuid = private Uuid of Guid

module Uuid =

    [<Literal>]
    let private regularExpressionForUuid =
        @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$"

    let tryFrom element =
        match element with
        | Null
        | NotMatches regularExpressionForUuid -> None
        | _ ->
            match Guid.TryParse element with
            | true, i -> Uuid i |> Some
            | _ -> None

    let from param = Uuid param

    let fromString element =
        match tryFrom element with
        | Some x -> x
        | None -> raise <| ArgumentException()

    let apply func (Uuid element) = func element

    let value element = apply id element

    let newUuid () = Uuid(Guid.NewGuid())

    let toString (element: Uuid) = (element |> value).ToString()

    module internal Model =

        let fromModel (element: Uuid) = (element |> value).ToString()

        let toModel (element: string) = element |> fromString

        let tryToModel element = tryFrom element

    module internal EntityFrameworkCore =

        let toModelExpression =
            <@ Func<Guid, Uuid>(from) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Guid, Uuid>>>

        let fromModelExpression =
            <@ Func<Uuid, Guid>(value) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Uuid, Guid>>>

        let toOption (element: Guid Nullable) =
            match element.HasValue with
            | false -> None
            | true -> Some(element.Value |> from)

        let toOptionExpression =
            <@ Func<Guid Nullable, Uuid option>(toOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Guid Nullable, Uuid option>>>

        let fromOption (element: Uuid option) =
            match element with
            | Some y -> y |> value |> Nullable
            | None -> Unchecked.defaultof<Guid Nullable>

        let fromOptionExpression =
            <@ Func<Uuid option, Guid Nullable>(fromOption) @>
            |> LeafExpressionConverter.QuotationToExpression
            |> unbox<Expression<Func<Uuid option, Guid Nullable>>>

    type UuidJsonConverter() =
        inherit JsonConverter<Uuid>()

        override this.Read(reader: byref<Utf8JsonReader>, typeToConvert: Type, options: JsonSerializerOptions) : Uuid =
            Model.toModel (reader.GetString())

        override this.Write(writer: Utf8JsonWriter, value: Uuid, options: JsonSerializerOptions) =
            writer.WriteStringValue(Model.fromModel value)

    type UuidTypeConverter() =
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

    type UuidValueConverter() =
        inherit ValueConverter<Uuid, Guid>(EntityFrameworkCore.fromModelExpression,
                                           EntityFrameworkCore.toModelExpression)

    type UuidOptionValueConverter() =
        inherit ValueConverter<Uuid option, Guid Nullable>(EntityFrameworkCore.fromOptionExpression,
                                                           EntityFrameworkCore.toOptionExpression)

    let initTypeConverter () =
        TypeDescriptor.AddAttributes(typeof<Uuid>, TypeConverterAttribute typeof<UuidTypeConverter>)
        |> ignore
