namespace Pippel.Type

open System

/// Represents a Percentage with a value between 0 (0%) and 1 (100%)
type Percentage = private Percentage of float

module Percentage =

    /// <summary>Creates a <c>Percentage option</c> from a <c>float</c>that represents a Percentage</summary>
    let tryCreate (s: float) =
        match s < 0.0 || s > 1.0 with
        | true -> None
        | false -> Some(Percentage s)


    /// <summary>Creates a <c>Percentage</c> from a <c>float</c> that represents a Percentage</summary>
    /// <exception cref="System.ArgumentException">Thrown when the input float is null or hasn't a valid format</exception>
    let create s =
        let idOpt = tryCreate s
        if idOpt.IsNone then raise <| ArgumentException()
        idOpt.Value


    /// <summary>Applies a function to a <c>Percentage</c></summary>
    let private apply f (Percentage e) = f e


    /// Gets the float value
    let value e = apply id e
