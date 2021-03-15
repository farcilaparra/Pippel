namespace Pippel.Tyche.Bet.Type

open System
open Pippel.Type.Validation

type ScoreWeight =
    private
    | ScoreWeight of int

    static member Create element =
        match element with
        | NotRange 0 99 -> Error "Value is out of range [0, 100)"
        | _ -> Ok <| ScoreWeight element

    static member TryFrom element =
        match ScoreWeight.Create element with
        | Ok it -> Some it
        | Error _ -> None

    static member TryFromNullable(element: int Nullable) =
        match element.HasValue with
        | false -> None
        | true -> Some <| ScoreWeight.From element.Value

    static member From element =
        match ScoreWeight.Create element with
        | Ok it -> it
        | Error message -> raise <| ArgumentException(message)

    static member FromNullable element =
        match ScoreWeight.TryFromNullable element with
        | Some it -> it
        | None -> raise <| ArgumentException("Number is null")

    member this.Value =
        match this with
        | ScoreWeight it -> it
