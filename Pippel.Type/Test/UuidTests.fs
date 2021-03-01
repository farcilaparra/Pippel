module UuidTests

open System
open Xunit
open Pippel.Type

[<Fact>]
let ``given a Guid when an Uuid is created from it then an Uuid is returned`` () =
    let value = Guid.NewGuid()
    let uuid = value |> Uuid.from
    Assert.Equal(value, uuid |> Uuid.value)

[<Fact>]
let ``given a valid string when an Uuid is created from it then an Uuid is returned`` () =
    let value = Guid.NewGuid().ToString()
    let uuid = value |> Uuid.fromString
    Assert.Equal(value, uuid |> Uuid.toString)

[<Fact>]
let ``given a invalid string when an Uuid is created from it then an exception is raised`` () =
    let value = ""
    Assert.Throws<ArgumentException>(fun () -> value |> Uuid.fromString |> ignore)

[<Fact>]
let ``given a valid string when an Uuid tries to create from it then an Uuid option with value is returned`` () =
    let value = Guid.NewGuid().ToString()
    let uuidOpt = value |> Uuid.tryFrom
    Assert.True(uuidOpt.IsSome)
    Assert.Equal(value, uuidOpt.Value |> Uuid.toString)

[<Fact>]
let ``given a invalid string when an Uuid tries to create from it then an Uuid option without value is returned`` () =
    let value = ""
    let uuidOpt = value |> Uuid.tryFrom
    Assert.True(uuidOpt.IsNone)

[<Fact>]
let ``given a guid nullable when an it's converted to Uuid option then an Uuid option is returned`` () =
    let provider = Guid.NewGuid() |> Nullable

    let model =
        Uuid.EntityFrameworkCore.toOption provider

    Assert.True(model.IsSome)
    Assert.Equal(provider.Value, model.Value |> Uuid.value)

[<Fact>]
let ``given a guid nullable with null when it's converted to Uuid option then an Uuid option is returned`` () =
    let provider = Nullable<Guid>()

    let model =
        Uuid.EntityFrameworkCore.toOption provider

    Assert.True(model.IsNone)

[<Fact>]
let ``given a Uuid option with value when it's converted to guid nullable then an guid nullable with value is returned``
    ()
    =
    let model = Some(Uuid.newUuid ())

    let provider =
        Uuid.EntityFrameworkCore.fromOption model

    Assert.True(provider.HasValue)
    Assert.Equal(model.Value |> Uuid.value, provider.Value)

[<Fact>]
let ``given a Uuid option with none when it's converted to guid nullable then an guid nullable with null is returned``
    ()
    =
    let model : Uuid option = None

    let provider =
        Uuid.EntityFrameworkCore.fromOption model

    Assert.True(not provider.HasValue)

[<Fact>]
let ``given a uuid string when it's converted to Uuid using model converter then an Uuid is returned`` () =
    let provider = Guid.NewGuid().ToString()
    let model = Uuid.Model.toModel provider
    Assert.Equal(provider, model |> Uuid.toString)

[<Fact>]
let ``given an uuid string when it tries to convert to Uuid using model converter then an Uuid option with value is returned``
    ()
    =
    let provider = Guid.NewGuid().ToString()
    let model = Uuid.Model.tryToModel provider
    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> Uuid.toString)

[<Fact>]
let ``given an invalid uuid string when it tries to convert to Uuid using model converter then an Uuid option without value is returned``
    ()
    =
    let provider = "hello"
    let model = Uuid.Model.tryToModel provider
    Assert.True(model.IsNone)

[<Fact>]
let ``given a Uuid when it's converted to uuid string using model converter then an uuid string is returned`` () =
    let model = Uuid.newUuid ()
    let provider = Uuid.Model.fromModel model
    Assert.Equal(model |> Uuid.toString, provider)
