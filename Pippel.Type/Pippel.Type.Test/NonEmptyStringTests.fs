module NonEmptyStringTests

open System
open Xunit
open Pippel.Type

let wrongValues: obj [] seq =
    seq {
        yield [| null |]
        yield [| "" |]
        yield [| " " |]
    }

[<Theory>]
[<InlineData("hello")>]
let ``given a non empty string when a NonEmptyString is created then a NonEmptyString is returned`` (value: string) =
    let nonEmptyString = value |> NonEmptyString.create
    Assert.True(true)
    Assert.Equal(value, nonEmptyString |> NonEmptyString.value)


[<Theory>]
[<MemberData("wrongValues")>]
let ``given an empty string when a NonEmptyString is created then an exception is raised`` (value: string) =
    Assert.Throws<ArgumentException>(fun () -> value |> NonEmptyString.create |> ignore)


[<Theory>]
[<InlineData("hello")>]
let ``given a non empty string when a NonEmptyString try to create then an option with the string is returned`` (value: string) =
    let nonEmptyStringOpt = value |> NonEmptyString.tryCreate
    Assert.True(nonEmptyStringOpt.IsSome)
    Assert.Equal(value, nonEmptyStringOpt.Value |> NonEmptyString.value)


[<Theory>]
[<MemberData("wrongValues")>]
let ``given an empty string when a NonEmptyString try to create then an option without value is returned`` (value: string) =
    let NonEmptyStringOpt = value |> NonEmptyString.tryCreate
    Assert.True(NonEmptyStringOpt.IsNone)
