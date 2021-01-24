module PositiveInt32Tests

open System
open Xunit
open Pippel.Type

let wrongValues: obj [] seq =
    seq {
        yield [| -1 |]
    }

[<Theory>]
[<InlineData(1)>]
let ``given a correct value for a PositiveInt32 when a PositiveInt32 is created then a PositiveInt32 is returned`` (value: int32) =
    let positiveInt32 = value |> PositiveInt32.create
    Assert.True(true)


[<Theory>]
[<MemberData(nameof(wrongValues))>]
let ``given a wrong value for a PositiveInt32 when a PositiveInt32 is created then an exception is raised`` (value: int32) =
    Assert.Throws<ArgumentException>(fun () -> value |> PositiveInt32.create |> ignore)


[<Theory>]
[<InlineData(1)>]
let ``given a correct value for a PositiveInt32 when a PositiveInt32 try to create then an option with the value is returned`` (value: int32) =
    let positiveInt32Opt = value |> PositiveInt32.tryCreate
    Assert.True(positiveInt32Opt.IsSome)
    Assert.Equal(value, positiveInt32Opt.Value |> PositiveInt32.value)


[<Theory>]
[<MemberData(nameof(wrongValues))>]
let ``given a wrong value for a PositiveInt32 when a PositiveInt32 try to create then an option without value is returned`` (value: int32) =
    let positiveInt32Opt = value |> PositiveInt32.tryCreate
    Assert.True(positiveInt32Opt.IsNone)
