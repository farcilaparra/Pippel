namespace Pippel.Type

open System

/// Represents a number higher or equal than 0
type PositiveInt = private PositiveInt of int32

module PositiveInt =

    /// <summary>Creates a <c>PositiveInt option</c> from a <c>int32</c>that represents a PositiveInt</summary>
    let tryCreate (s: int32) =
        match s < 0 with
        | true -> None
        | false -> Some(PositiveInt s)


    /// <exception cref="System.ArgumentException">Thrown when the input is less that 0</exception>
    let create s =
        let idOpt = tryCreate s
        if idOpt.IsNone then raise <| ArgumentException()
        idOpt.Value


    /// <summary>Applies a function to a <c>PositiveInt</c></summary>
    let private apply f (PositiveInt e) = f e


    /// Gets the int32 value
    let value e = apply id e
