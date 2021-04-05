namespace Pippel.Type

open System
open Validation

type PositiveInt =
    private
    | PositiveInt of int

    static member Create element =
        match element with
        | Less 0 -> Error "Number is less that zero"
        | _ -> Ok <| PositiveInt element

    static member TryFrom element =
        match PositiveInt.Create element with
        | Ok it -> Some it
        | Error _ -> None

    static member From element =
        match PositiveInt.Create element with
        | Ok it -> it
        | Error message -> raise <| ArgumentException(message)

    static member TryFromNullable(element: int Nullable) =
        match element.HasValue with
        | false -> None
        | true -> Some <| PositiveInt.From element.Value

    static member FromNullable element =
        match PositiveInt.TryFromNullable element with
        | Some it -> it
        | None -> raise <| ArgumentException("Number is null")

    member this.Value =
        match this with
        | PositiveInt it -> it

    static member (+)(a, b) =
        match (a, b) with
        | PositiveInt itx, PositiveInt ity -> PositiveInt.From(itx + ity)

    static member (-)(a, b) =
        match (a, b) with
        | PositiveInt itx, PositiveInt ity -> PositiveInt.From(itx - ity)

    static member (*)(a, b) =
        match (a, b) with
        | PositiveInt itx, PositiveInt ity -> PositiveInt.From(itx * ity)

    static member (/)(a, b) =
        match (a, b) with
        | PositiveInt itx, PositiveInt ity -> PositiveInt.From(itx / ity)

[<RequireQualifiedAccess>]
module Number =

    let inline tryFrom (element: ^SourceType when ^SourceType: unmanaged): ^Type option = TypeBuilder.tryFrom element

    let inline from (element: ^SourceType when ^SourceType: unmanaged): ^Type = TypeBuilder.from element

    let inline value (element: ^Type): ^SourceType when ^SourceType: unmanaged = TypeBuilder.value element

    let inline toString (element: ^Type): string = TypeBuilder.toString element

    let inline nullableValue (element: ^Type option): ^SourceType Nullable when ^SourceType: unmanaged =
        TypeBuilder.nullableValue element
