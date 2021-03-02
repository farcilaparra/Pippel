module PercentageTests

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
    let Percentage = value |> Percentage.from
    Assert.True(true)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a Percentage when a Percentage is created then an exception is raised`` (value: float) =
    Assert.Throws<ArgumentException>(fun () -> value |> Percentage.from |> ignore)

[<Fact>]
let ``given a correct value for a Percentage when a Percentage tries to create then an option with the value is returned``
    ()
    =
    let value = 0.5
    let percentageOpt = value |> Percentage.tryFrom
    Assert.True(percentageOpt.IsSome)
    Assert.Equal(value, percentageOpt.Value |> Percentage.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a Percentage when a Percentage tries to create then an option without value is returned``
    (value: float)
    =
    let percentageOpt = value |> Percentage.tryFrom
    Assert.True(percentageOpt.IsNone)

[<Fact>]
let ``given a valid float when an its' converted to Percentage option then a Percentage option is returned`` () =
    let provider = 0.5 |> Nullable

    let model =
        Percentage.EntityFrameworkCore.toOption provider

    Assert.True(model.IsSome)
    Assert.Equal(model.Value |> Percentage.value, provider.Value)

[<Fact>]
let ``given a null float when it's converted to Percentage option then a Percentage option with none is returned`` () =
    let provider = Nullable<float>()

    let model =
        Percentage.EntityFrameworkCore.toOption provider

    Assert.True(model.IsNone)

[<Fact>]
let ``given a Percentage option with value when an it's converted to float nullable then a float nullable with value is returned``
    ()
    =
    let model = Some(0.5 |> Percentage.from)

    let provider =
        Percentage.EntityFrameworkCore.fromOption model

    Assert.True(provider.HasValue)
    Assert.Equal(model.Value |> Percentage.value, provider.Value)

[<Fact>]
let ``given a Percentage option with none when an it's converted to float nullable then an float nullable with null is returned``
    ()
    =
    let model : Percentage option = None

    let provider =
        Percentage.EntityFrameworkCore.fromOption model

    Assert.True(not provider.HasValue)

[<Fact>]
let ``given a valid float string when it's converted to Percentage using json converter then a Percentage is returned``
    ()
    =
    let provider = (0.5).ToString()
    let model = Percentage.Model.toModel provider
    Assert.Equal(provider, model |> Percentage.toString)

[<Fact>]
let ``given a valid float string when it tries to convert to Percentage using json converter then a Percentage option with value is returned``
    ()
    =
    let provider = (0.5).ToString()
    let model = Percentage.Model.tryToModel provider
    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> Percentage.toString)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an invalid float string when it tries to convert to Percentage using json converter then a Percentage option without value is returned``
    (value: float)
    =
    let provider = value.ToString()
    let model = Percentage.Model.tryToModel provider
    Assert.True(model.IsNone)

[<Fact>]
let ``given a Percentage when it's converted to string using json converter then a float string is returned`` () =
    let model = 0.5 |> Percentage.from
    let provider = Percentage.Model.fromModel model
    Assert.Equal(model |> Percentage.toString, provider)
