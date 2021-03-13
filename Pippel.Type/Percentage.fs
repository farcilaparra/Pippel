namespace Pippel.Type

open System
open Validation

type Percentage = private Percentage of float

module Percentage =

    let tryFrom element =
        match element with
        | NotRange 0.0 1.0 -> None
        | _ -> Percentage element |> Some

    let from element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (Percentage element) = func element

    let value element = apply id element

    let toString (element: Percentage) = (element |> value).ToString()
