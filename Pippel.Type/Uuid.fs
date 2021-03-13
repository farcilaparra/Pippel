namespace Pippel.Type

open System
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
