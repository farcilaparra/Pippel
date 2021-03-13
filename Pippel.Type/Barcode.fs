namespace Pippel.Type

open System
open System.Linq
open Validation

type Barcode = private Barcode of string

module Barcode =

    [<Literal>]
    let private regularExpressionForBarcode = @"^(\d{8}|\d{12,14})$"

    let (|NotChekSum|_|) (value: string) =
        let paddedCode = value.PadLeft(14, '0')

        let sum =
            paddedCode
                .Select(fun c i ->
                    Int32.Parse(c.ToString())
                    * (match i % 2 = 0 with
                       | true -> 3
                       | false -> 1))
                .Sum()

        (sum % 10) = 0 |> not |> ifTrueThen NotChekSum

    let tryFrom element =
        match element with
        | Null
        | NotMatches regularExpressionForBarcode
        | NotChekSum -> None
        | _ -> Barcode element |> Some

    let from element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException()

    let apply func (Barcode element) = func element

    let value element = apply id element

    let toString (element: Barcode) = (element |> value).ToString()
