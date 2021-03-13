namespace Pippel.Type

open System
open Validation

[<CustomEquality>]
[<CustomComparison>]
type DateTime = private DateTime of System.DateTime with

    override this.GetHashCode() =
        match this with DateTime it -> it.GetHashCode()
    
    override this.Equals(obj) =
        match (this, obj :?> DateTime) with DateTime a, DateTime b -> a.Equals b
    
    interface IComparable with

        member this.CompareTo(obj) =
            match (this, obj :?> DateTime) with DateTime a, DateTime b -> a.CompareTo b    

module DateTime =

    [<Literal>]
    let private regularExpressionForDateTime =
        @"^(?:[1-9]\d{3}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1\d|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[1-9]\d(?:0[48]|[2468][048]|[13579][26])|(?:[2468][048]|[13579][26])00)-02-29)T(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d(?:\.\d{1,9})?(?:Z|[+-][01]\d:[0-5]\d)$"

    let tryFrom element =
        match element with
        | Null
        | NotMatches regularExpressionForDateTime -> None
        | _ ->
            match DateTime.TryParse(element) with
            | true, d -> DateTime d |> Some
            | _ -> None

    let from element = DateTime element

    let fromString element =
        match tryFrom element with
        | Some i -> i
        | None -> raise <| ArgumentException("Invalid format")

    let apply func (DateTime element) = func element

    let value element = apply id element

    let now = System.DateTime.Now |> from

    let toString (element: DateTime) = (element |> value).ToString("o")

    let toStringWithFormat (format: string) (element: DateTime) = (element |> value).ToString(format)

    let toUniversalTime (element: DateTime) =
        (element |> value).ToUniversalTime() |> from

    let addDays daysCount element =
        (element |> value).AddDays daysCount |> from
    