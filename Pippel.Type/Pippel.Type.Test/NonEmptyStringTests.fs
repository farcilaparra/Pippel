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
