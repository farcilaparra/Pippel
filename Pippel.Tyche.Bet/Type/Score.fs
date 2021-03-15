namespace Pippel.Tyche.Bet.Type

open System
open Pippel.Type.Validation

type Score =
    private
    | Score of int

    static member Create element =
        match element with
        | NotRange 0 999 -> Error "Value is out of range [0, 1000)"
        | _ -> Ok <| Score element

    static member TryFrom element =
        match Score.Create element with
        | Ok it -> Some it
        | Error _ -> None

    static member TryFromNullable(element: int Nullable) =
        match element.HasValue with
        | false -> None
        | true -> Some <| Score.From element.Value

    static member From element =
        match Score.Create element with
        | Ok it -> it
        | Error message -> raise <| ArgumentException(message)

    static member FromNullable element =
        match Score.TryFromNullable element with
        | Some it -> it
        | None -> raise <| ArgumentException("Number is null")

    member this.Value =
        match this with
        | Score it -> it
