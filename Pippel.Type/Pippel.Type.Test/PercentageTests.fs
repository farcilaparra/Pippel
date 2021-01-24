module PercentageTests

open System
open Xunit
open Pippel.Type

let wrongValues: obj [] seq =
    seq {
        yield [| -0.1 |]
        yield [| 1.1 |]
    }

[<Theory>]
[<InlineData(0.5)>]
let ``given a correct value for a Percentage when a Percentage is created then a percentage is returned`` (value: float) =
    let Percentage = value |> Percentage.create
    Assert.True(true)


[<Theory>]
[<MemberData("wrongValues")>]
let ``given a wrong value for a Percentage when a Percentage is created then an exception is raised`` (value: float) =
    Assert.Throws<ArgumentException>(fun () -> value |> Percentage.create |> ignore)


[<Theory>]
[<InlineData(0.5)>]
let ``given a correct value for a Percentage when a Percentage try to create then an option with the value is returned`` (value: float) =
    let percentageOpt = value |> Percentage.tryCreate
    Assert.True(percentageOpt.IsSome)
    Assert.Equal(value, percentageOpt.Value |> Percentage.value)


[<Theory>]
[<MemberData("wrongValues")>]
let ``given a wrong value for a Percentage when a Percentage try to create then an option without value is returned`` (value: float) =
    let percentageOpt = value |> Percentage.tryCreate
    Assert.True(percentageOpt.IsNone)
