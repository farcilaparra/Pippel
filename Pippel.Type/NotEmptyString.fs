namespace Pippel.Type

open System
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
