namespace Pippel.Type

open System
open System.Linq

/// Represents a barcode
type Barcode = private Barcode of string

module Barcode =

    [<Literal>]
    let private regularExpressionForBarcode = @"^(\d{8}|\d{12,14})$"


    /// Determines if a string represents a valid barcode
    let private isValidGtin (code: string) =
        let paddedCode = code.PadLeft(14, '0')

        let sum =
            paddedCode.Select(fun c i ->
                      Int32.Parse(c.ToString())
                      * (match i % 2 = 0 with
                         | true -> 3
                         | false -> 1)).Sum()

        (sum % 10) = 0


    /// <summary>Creates a <c>Barcode option</c> from a <c>string</c>that represents a barcode</summary>
    let tryCreate (s: string) =
        match isNull s with
        | true -> None
        | false ->
            match System.Text.RegularExpressions.Regex.IsMatch(s, regularExpressionForBarcode) with
            | true ->
                match isValidGtin (s) with
                | true -> Some(Barcode s)
                | false -> None
            | false -> None


    /// <summary>Creates a <c>Barcode</c> from a <c>string</c> that represents a barcode</summary>
    /// <exception cref="System.ArgumentException">Thrown when the input string is null or hasn't a valid format</exception>
    let create s =
        let idOpt = tryCreate s
        if idOpt.IsNone then raise <| ArgumentException()
        idOpt.Value


    /// <summary>Applies a function to a <c>Barcode</c></summary>
    let private apply f (Barcode e) = f e


    /// Gets the string value
    let value e = apply id e
