module NonEmptyStringTests

open System
open Xunit
open Pippel.Type

let wrongValues : obj [] seq =
    seq {
        yield [| null |]
        yield [| "" |]
        yield [| " " |]
    }

[<Fact>]
let ``given a non empty string when a NonEmptyString is created then a NonEmptyString is returned`` () =
    let value = "hello"
    let nonEmptyString = value |> NotEmptyString.from
    Assert.True(true)
    Assert.Equal(value, nonEmptyString |> NotEmptyString.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an empty string when a NonEmptyString is created then an exception is raised`` (value: string) =
    Assert.Throws<ArgumentException>(fun () -> value |> NotEmptyString.from |> ignore)

[<Fact>]
let ``given a non empty string when a NonEmptyString tries to create then an option with the string is returned`` () =
    let value = "hello"
    let nonEmptyStringOpt = value |> NotEmptyString.tryFrom
    Assert.True(nonEmptyStringOpt.IsSome)
    Assert.Equal(value, nonEmptyStringOpt.Value |> NotEmptyString.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an empty string when a NonEmptyString tries to create then an option without value is returned``
    (value: string)
    =
    let NonEmptyStringOpt = value |> NotEmptyString.tryFrom
    Assert.True(NonEmptyStringOpt.IsNone)

[<Fact>]
let ``given a non empty string when it's converted to NonEmptyString option then a NonEmptyString option with value is returned``
    ()
    =
    let provider = "hello"

    let model =
        NotEmptyString.EntityFrameworkCore.toOption provider

    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> NotEmptyString.value)

[<Fact>]
let ``given a null string when it's converted to NonEmptyString option then a NonEmptyString option with none is returned``
    ()
    =
    let provider : string = null

    let model =
        NotEmptyString.EntityFrameworkCore.toOption provider

    Assert.True(model.IsNone)

[<Fact>]
let ``given a NonEmptyString option with value when an it's converted to string then a string with value is returned``
    ()
    =
    let model = Some("hello" |> NotEmptyString.from)

    let provider =
        NotEmptyString.EntityFrameworkCore.fromOption model

    Assert.True(not (isNull provider))
    Assert.Equal(model.Value |> NotEmptyString.value, provider)

[<Fact>]
let ``given a NonEmptyString option with none when an it's converted to string then a string with null is returned``
    ()
    =
    let model : NotEmptyString option = None

    let provider =
        NotEmptyString.EntityFrameworkCore.fromOption model

    Assert.True(isNull provider)

[<Fact>]
let ``given a non empty string when it's converted to NonEmptyString using json converter then a NonEmptyString is returned``
    ()
    =
    let provider = "Hello"
    let model = NotEmptyString.Model.toModel provider
    Assert.Equal(provider, model |> NotEmptyString.toString)

[<Fact>]
let ``given a non empty string when it tries to convert to NonEmptyString using json converter then a NonEmptyString option with value is returned``
    ()
    =
    let provider = "Hello"
    let model = NotEmptyString.Model.tryToModel provider
    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> NotEmptyString.toString)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an invalid non empty string when it tries to convert to NonEmptyString using json converter then a NonEmptyString option without value is returned``
    (provider: string)
    =
    let model = NotEmptyString.Model.tryToModel provider
    Assert.True(model.IsNone)

[<Fact>]
let ``given a NonEmptyString when it's converted to string using json converter then a string is returned`` () =
    let model = "Hello" |> NotEmptyString.from
    let provider = NotEmptyString.Model.fromModel model
    Assert.Equal(model |> NotEmptyString.toString, provider)
