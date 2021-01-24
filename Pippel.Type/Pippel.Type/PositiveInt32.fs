namespace Pippel.Type

open System

/// Represents a number higher or equal than 0
type PositiveInt32 = private PositiveInt32 of int32

module PositiveInt32 =

    /// <summary>Creates a <c>PositiveInt32 option</c> from a <c>int32</c>that represents a PositiveInt32</summary>
    let tryCreate (s: int32) =
        match s < 0 with
        | true -> None
        | false -> Some(PositiveInt32 s)


    /// <exception cref="System.ArgumentException">Thrown when the input is less that 0</exception>
    let create s =
        let idOpt = tryCreate s
        if idOpt.IsNone then raise <| ArgumentException()
        idOpt.Value


    /// <summary>Applies a function to a <c>PositiveInt32</c></summary>
    let private apply f (PositiveInt32 e) = f e


    /// Gets the float value
    let value e = apply id e
