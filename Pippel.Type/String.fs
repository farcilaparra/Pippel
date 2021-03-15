namespace Pippel.Type

open System
open Validation

type NotEmptyString =
    private
    | NotEmptyString of string

    static member Create element =
        match element with
        | Null -> Error "String is null"
        | WhiteSpaces -> Error "String is empty or has white spaces"
        | _ -> Ok <| NotEmptyString element

    static member TryFrom element =
        match NotEmptyString.Create element with
        | Ok it -> Some it
        | Error _ -> None

    static member From element =
        match NotEmptyString.Create element with
        | Ok it -> it
        | Error message -> raise <| ArgumentException(message)

    member this.Value =
        match this with
        | NotEmptyString it -> it

type NotEmptyString100 =
    private
    | NotEmptyString100 of string

    static member Create element =
        match element with
        | Null -> Error "String is null"
        | WhiteSpaces -> Error "String is empty or has white spaces"
        | HasMoreCharsThan 100 -> Error "String has more than 100 chars"
        | _ -> Ok <| NotEmptyString100 element

    static member TryFrom element =
        match NotEmptyString100.Create element with
        | Ok it -> Some it
        | Error _ -> None

    static member From element =
        match NotEmptyString100.Create element with
        | Ok it -> it
        | Error message -> raise <| ArgumentException(message)

    member this.Value =
        match this with
        | NotEmptyString100 it -> it

[<RequireQualifiedAccess>]
module String =

    let inline tryFrom (element: ^SourceType) : ^Type option = TypeBuilder.tryFrom element

    let inline from (element: ^SourceType) : ^Type = TypeBuilder.from element

    let inline value (element: ^Type) : string = TypeBuilder.value element

    let inline toString (element: ^Type) : string = TypeBuilder.toString element
