namespace Pippel.Tyche.Bet.Api

open System
open Pippel.Tyche.Bet

module Exception =

    [<Literal>]
    let private prefixCode = @"TYCHE"

    type ExceptionCode =
        | Generic = 0
        | EditingBetNotAllowed = 1

    let funcCreateCustomCode (ex: Exception) : string =
        match ex with
        | :? EditingBetNotAllowedException as ex ->
            $"{prefixCode}-{string (LanguagePrimitives.EnumToValue ExceptionCode.EditingBetNotAllowed)}"
        | _ -> $"{prefixCode}-{string (LanguagePrimitives.EnumToValue ExceptionCode.Generic)}"
