module NonEmptyString100Tests

open System
open Xunit
open Pippel.Type

let wrongValues : obj [] seq =
    seq {
        yield [| null |]
        yield [| "" |]
        yield [| " " |]
        yield [| "c" |> String.replicate 101 |]
    }

[<Fact>]
let ``given a valid string for NonEmptyString100 when a NonEmptyString100 is created then a NonEmptyString100 is returned``
    ()
    =
    let value = "hello"
    let nonEmptyString = NotEmptyString100.From value
    Assert.Equal(value, nonEmptyString |> String.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an invalid string for NonEmptyString100 when a NonEmptyString100 is created then an exception is raised``
    (value: string)
    =
    Assert.Throws<ArgumentException>(fun () -> NotEmptyString100.From value |> ignore)

[<Fact>]
let ``given an invalid string for NonEmptyString100 when a NonEmptyString100 tries to create then an option with the string is returned``
    ()
    =
    let value = "hello"
    let nonEmptyStringOpt = NotEmptyString100.TryFrom value
    Assert.True(nonEmptyStringOpt.IsSome)
    Assert.Equal(value, nonEmptyStringOpt.Value |> String.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given an invalid string for NonEmptyString100 when a NonEmptyString100 tries to create then an option without value is returned``
    (value: string)
    =
    let NonEmptyStringOpt = NotEmptyString100.TryFrom value
    Assert.True(NonEmptyStringOpt.IsNone)
