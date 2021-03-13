namespace Pippel.Type

open System
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
        | None -> raise <| ArgumentException("Invalid format")

    let tryFromNullable (element: int Nullable) =
        match element.HasValue with
        | false -> None
        | true -> Some(element.Value |> from)

    let fromNullable element =
        match tryFromNullable element with
        | Some it -> it
        | None -> raise <| ArgumentException("Invalid format")

    let apply func (PositiveInt element) = func element

    let value element = apply id element

    let nullableValue element =
        match element with
        | Some it -> it |> value |> Nullable
        | None -> Unchecked.defaultof<int Nullable>

    let toString (element: PositiveInt) = (element |> value).ToString()
