namespace Pippel.Type

open System

/// Represents a non nullable or non empty string
type NonEmptyString = private NonEmptyString of string

module NonEmptyString =

    /// <summary>Creates a <c>NonEmptyString option</c> from a <c>string</c></summary>
    let tryCreate (s: string) =
        match String.IsNullOrWhiteSpace(s) with
        | true -> None
        | false -> Some(NonEmptyString s)


    /// <summary>Creates a <c>NonEmptyString</c> from a <c>string</c></summary>
    /// <exception cref="System.ArgumentException">Thrown when the input string is null</exception>
    let create s =
        let idOpt = tryCreate s
        if idOpt.IsNone then raise <| ArgumentException()
        idOpt.Value


    /// <summary>Applies a function to a <c>NonEmptyString</c></summary>
    let private apply f (NonEmptyString e) = f e


    /// Gets the string value
    let value e = apply id e