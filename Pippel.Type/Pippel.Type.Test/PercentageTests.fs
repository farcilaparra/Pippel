﻿module PercentageTests

open System
open Xunit
open Pippel.Type

let wrongValues : obj [] seq =
    seq {
        yield [| -0.1 |]
        yield [| 1.1 |]
    }

[<Fact>]
let ``given a correct value for a Percentage when a Percentage is created then a percentage is returned`` () =
    let value = 0.5
    let Percentage = Percentage.From value
    Assert.True(true)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a Percentage when a Percentage is created then an exception is raised`` (value: float) =
    Assert.Throws<ArgumentException>(fun () -> Percentage.From value |> ignore)

[<Fact>]
let ``given a correct value for a Percentage when a Percentage tries to create then an option with the value is returned``
    ()
    =
    let value = 0.5
    let percentageOpt = Percentage.TryFrom value
    Assert.True(percentageOpt.IsSome)
    Assert.Equal(value, percentageOpt.Value |> Number.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a Percentage when a Percentage tries to create then an option without value is returned``
    (value: float)
    =
    let percentageOpt = Percentage.TryFrom value
    Assert.True(percentageOpt.IsNone)
