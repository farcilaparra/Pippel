namespace Pippel.Tyche.Bet

open System

module Mapper =

    type TryMapAndReRaiseBuilder() =

        member this.Bind(x, f) =
            try
                f x
            with
            | :? ArgumentException as ex -> raise <| DomainValueException(ex.Message)
            | _ -> reraise ()

        member this.Return x = x

        member this.ReturnFrom x = x

    let tryMap = TryMapAndReRaiseBuilder()
