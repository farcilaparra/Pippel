module PositiveIntTests

open System
open Pippel.Type
open Xunit

let wrongValues : obj [] seq = seq { yield [| -1 |] }

[<Fact>]
let ``given a correct value for a PositiveInt when a PositiveInt is created then a PositiveInt is returned`` () =
    let value = 1
    let positiveInt32 = PositiveInt.From value
    Assert.True(true)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a PositiveInt when a PositiveInt is created then an exception is raised`` (value: int32) =
    Assert.Throws<ArgumentException>(fun () -> PositiveInt.From value |> ignore)

[<Fact>]
let ``given a correct value for a PositiveInt when a PositiveInt tries to create then an option with the value is returned``
    ()
    =
    let value = 1
    let positiveInt32Opt = PositiveInt.TryFrom value
    Assert.True(positiveInt32Opt.IsSome)
    Assert.Equal(value, positiveInt32Opt.Value |> Number.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a PositiveInt when a PositiveInt tries to create then an option without value is returned``
    (value: int32)
    =
    let positiveInt32Opt = PositiveInt.TryFrom value
    Assert.True(positiveInt32Opt.IsNone)
