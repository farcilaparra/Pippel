namespace Pippel.Tyche.Bet.Api

open System
open Pippel.Core.Exception
open Pippel.Tyche.Bet

module Exception =

    [<Literal>]
    let private prefixCode = @"TYCHE"

    let private exceptionsMap =
        Map [ (typeof<DomainValueException>.Name, 1)
              (typeof<EditingBetNotAllowedException>.Name, 2) ]

    let funcCreateCustomCode (ex: Exception) : string option =
        match exceptionsMap.TryFind <| ex.GetType().Name with
        | Some it -> Some <| formatException prefixCode it
        | None -> None
