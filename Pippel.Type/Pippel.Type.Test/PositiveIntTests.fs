module PositiveIntTests

open System
open Pippel.Type
open Xunit

let wrongValues : obj [] seq = seq { yield [| -1 |] }

[<Fact>]
let ``given a correct value for a PositiveInt when a PositiveInt is created then a PositiveInt is returned`` () =
    let value = 1
    let positiveInt32 = value |> PositiveInt.from
    Assert.True(true)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a PositiveInt when a PositiveInt is created then an exception is raised`` (value: int32) =
    Assert.Throws<ArgumentException>(fun () -> value |> PositiveInt.from |> ignore)

[<Fact>]
let ``given a correct value for a PositiveInt when a PositiveInt tries to create then an option with the value is returned``
    ()
    =
    let value = 1
    let positiveInt32Opt = value |> PositiveInt.tryFrom
    Assert.True(positiveInt32Opt.IsSome)
    Assert.Equal(value, positiveInt32Opt.Value |> PositiveInt.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a PositiveInt when a PositiveInt tries to create then an option without value is returned``
    (value: int32)
    =
    let positiveInt32Opt = value |> PositiveInt.tryFrom
    Assert.True(positiveInt32Opt.IsNone)

[<Fact>]
let ``given a valid int when an it's converted to PositiveInt option then a PositiveInt option is returned`` () =
    let provider = 1 |> Nullable

    let model =
        PositiveInt.EntityFrameworkCore.toOption provider

    Assert.True(model.IsSome)
    Assert.Equal(model.Value |> PositiveInt.value, provider.Value)

[<Fact>]
let ``given a null int when it's converted to PositiveInt option then a PositiveInt option with none is returned`` () =
    let provider = Nullable<int>()

    let model =
        PositiveInt.EntityFrameworkCore.toOption provider

    Assert.True(model.IsNone)

[<Fact>]
let ``given a PositiveInt option with value when an it's converted to int nullable then an int nullable with value is returned``
    ()
    =
    let model = Some(1 |> PositiveInt.from)

    let provider =
        PositiveInt.EntityFrameworkCore.fromOption model

    Assert.True(provider.HasValue)
    Assert.Equal(model.Value |> PositiveInt.value, provider.Value)

[<Fact>]
let ``given a PositiveInt option with none when an it's converted to int nullable then an int nullable with null is returned``
    ()
    =
    let model : PositiveInt option = None

    let provider =
        PositiveInt.EntityFrameworkCore.fromOption model

    Assert.True(not provider.HasValue)

[<Fact>]
let ``given a valid int string when it's converted to PositiveInt using model converter then a PositiveInt is returned``
    ()
    =
    let provider = (1).ToString()
    let model = PositiveInt.Model.toModel provider
    Assert.Equal(provider, model |> PositiveInt.toString)

[<Fact>]
let ``given a valid int string when it tries to convert to PositiveInt using model converter then a PositiveInt option with value is returned``
    ()
    =
    let provider = (1).ToString()
    let model = PositiveInt.Model.tryToModel provider
    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> PositiveInt.toString)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an invalid int string when it tries to convert to PositiveInt using model converter then a PositiveInt option without value is returned``
    (value: int)
    =
    let provider = value.ToString()
    let model = PositiveInt.Model.tryToModel provider
    Assert.True(model.IsNone)

[<Fact>]
let ``given a PositiveInt when it's converted to string using model converter then a int string is returned`` () =
    let model = 1 |> PositiveInt.from
    let provider = PositiveInt.Model.fromModel model
    Assert.Equal(model |> PositiveInt.toString, provider)
