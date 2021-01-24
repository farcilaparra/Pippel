namespace Pippel.Type

open System

/// Represents an Universally Unique Identifier
type Uuid = private Uuid of string

module Uuid =

    [<Literal>]
    let private regularExpressionForUuid =
        @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$"

    /// <summary>Creates an <c>Uuid option</c> from a <c>string</c> that represents an uuid</summary>
    let tryCreate (s: string) =
        match isNull s with
        | true -> None
        | false ->
            match System.Text.RegularExpressions.Regex.IsMatch(s, regularExpressionForUuid) with
            | true -> Some(Uuid s)
            | false -> None


    /// <summary>Creates an <c>Uuid</c> from a <c>string</c> that represents an uuid</summary>
    /// <exception cref="System.ArgumentException">Thrown when the input string is null or hasn't a valid format</exception>
    let create s =
        let idOpt = tryCreate s
        if idOpt.IsNone then
            ArgumentException "The string hasn't a valid format for a uuid"
            |> raise
        idOpt.Value


    /// <summary>Creates an <c>Uuid</c> from a <c>Guid</c></summary>
    let createFromGuid (s: Guid) = create (s.ToString())


    /// <summary>Applies a function to an <c>Uuid</c></summary>
    let private apply f (Uuid e) = f e


    /// Gets the string value
    let value e = apply id e
    
    
    /// <summary>Converts an <c>Uuid</c> to <c>Guid</c></summary> 
    let toGuid (s: Uuid) = Guid(s |> value)


    /// Gets a random uuid
    let newUuid () = Uuid(Guid.NewGuid().ToString())
