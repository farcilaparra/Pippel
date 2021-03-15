﻿namespace Pippel.Type

open System
open Pippel.Type
open Validation

type Percentage =
    private
    | Percentage of float

    static member Create element =
        match element with
        | NotRange 0.0 1.0 -> Error "Value is out of range [0, 1]"
        | _ -> Ok <| Percentage element

    static member TryFrom element =
        match Percentage.Create element with
        | Ok it -> Some it
        | Error _ -> None

    static member From element =
        match Percentage.Create element with
        | Ok it -> it
        | Error message -> raise <| ArgumentException(message)

    member this.Value =
        match this with
        | Percentage it -> it
