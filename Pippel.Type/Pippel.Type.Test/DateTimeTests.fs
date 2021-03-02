module DateTimeTests

open System
open Xunit
open Pippel.Type

[<Fact>]
let ``given a System.DateTime when a DateTime is created then a DateTime is returned`` () =
    let value = System.DateTime.Now
    let dateTime = value |> DateTime.from
    Assert.True(true)
    Assert.Equal(value, dateTime |> DateTime.value)

[<Fact>]
let ``given a System.DateTime nullable when it's converted to DateTime option then a DateTime option with value is returned``
    ()
    =
    let provider = System.DateTime.Now |> Nullable

    let model =
        DateTime.EntityFrameworkCore.toOption provider

    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> DateTime.value)

[<Fact>]
let ``given a null System.DateTime nullable when it's converted to DateTime option then a DateTime option with none is returned``
    ()
    =
    let provider : System.DateTime Nullable = Nullable<System.DateTime>()

    let model =
        DateTime.EntityFrameworkCore.toOption provider

    Assert.True(model.IsNone)

[<Fact>]
let ``given a DateTime option with value when an it's converted to string then an string with value is returned`` () =
    let model = Some(DateTime.now)

    let provider =
        DateTime.EntityFrameworkCore.fromOption model

    Assert.True(provider.HasValue)
    Assert.Equal(model.Value |> DateTime.value, provider.Value)

[<Fact>]
let ``given a DateTime option with none when an it's converted to string then an string with null is returned`` () =
    let model : DateTime option = None

    let provider =
        DateTime.EntityFrameworkCore.fromOption model

    Assert.True(not provider.HasValue)

[<Theory>]
[<InlineData("2021-02-21T14:48:00.00000Z", "yyyy-MM-ddTHH:mm:ss.fffffZ")>]
[<InlineData("2021-02-21T22:20:47.00000+00:00", "yyyy-MM-ddTHH:mm:ss.fffffzzz")>]
let ``given valid string when it's converted to DateTime using model converter then an DateTime is returned``
    (
        provider: string,
        format: string
    ) =
    let model = DateTime.Model.toModel provider

    Assert.Equal(
        provider,
        model
        |> DateTime.toUniversalTime
        |> DateTime.toStringWithFormat format
    )

[<Theory>]
[<InlineData("2021-02-21T14:48:00.00000Z", "yyyy-MM-ddTHH:mm:ss.fffffZ")>]
[<InlineData("2021-02-21T22:20:47.00000+00:00", "yyyy-MM-ddTHH:mm:ss.fffffzzz")>]
let ``given a valid string when it tries to convert to DateTime using model converter then an DateTime option with value is returned``
    (
        provider: string,
        format: string
    ) =
    let model = DateTime.Model.tryToModel provider

    Assert.True(model.IsSome)

    Assert.Equal(
        provider,
        model.Value
        |> DateTime.toUniversalTime
        |> DateTime.toStringWithFormat format
    )

[<Fact>]
let ``given a invalid string when it tries to convert to DateTime using model converter then an DateTime option without value is returned``
    ()
    =
    let provider = "hello"
    let model = DateTime.Model.tryToModel provider
    Assert.True(model.IsNone)

[<Fact>]
let ``given a DateTime when it's converted to string using model converter then an string is returned`` () =
    let model = DateTime.now
    let provider = DateTime.Model.fromModel model

    Assert.Equal(
        model
        |> DateTime.toUniversalTime
        |> DateTime.toStringWithFormat ("o"),
        provider
    )
