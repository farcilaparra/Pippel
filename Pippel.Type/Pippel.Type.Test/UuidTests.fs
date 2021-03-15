module UuidTests

open System
open Xunit
open Pippel.Type

[<Fact>]
let ``given a Guid when an Uuid is created from it then an Uuid is returned`` () =
    let value = Guid.NewGuid()
    let uuid = Uuid.From value
    Assert.Equal(value, uuid |> Uuid.value)

[<Fact>]
let ``given a valid string when an Uuid is created from it then an Uuid is returned`` () =
    let value = Guid.NewGuid().ToString()
    let uuid = Uuid.From value
    Assert.Equal(value, uuid |> Uuid.toString)

[<Fact>]
let ``given a invalid string when an Uuid is created from it then an exception is raised`` () =
    let value = ""
    Assert.Throws<ArgumentException>(fun () -> Uuid.From value |> ignore)

[<Fact>]
let ``given a valid string when an Uuid tries to create from it then an Uuid option with value is returned`` () =
    let value = Guid.NewGuid().ToString()
    let uuidOpt = Uuid.TryFrom value
    Assert.True(uuidOpt.IsSome)
    Assert.Equal(value, uuidOpt.Value |> Uuid.toString)

[<Fact>]
let ``given a invalid string when an Uuid tries to create from it then an Uuid option without value is returned`` () =
    let value = ""
    let uuidOpt = Uuid.TryFrom value
    Assert.True(uuidOpt.IsNone)
