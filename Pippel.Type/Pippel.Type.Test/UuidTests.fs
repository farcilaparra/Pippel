module UuidTests

open System
open Xunit
open Pippel.Type

let wrongValues: obj [] seq =
    seq {
        yield [| null |]
        yield [| "hello" |]
    }

[<Fact>]
let ``given a correct value for an Uuid when an Uuid is created from it then an Uuid is returned`` () =
    let value = "123e4567-e89b-12d3-a456-426655440000"
    let uuid = value |> Uuid.create
    Assert.True(true)
    Assert.Equal(value, uuid |> Uuid.value)


[<Theory>]
[<MemberData("wrongValues")>]
let ``given a wrong value for an Uuid when a Uuid is created from it then an exception is raised`` (value: string) =
    Assert.Throws<ArgumentException>(fun () -> value |> Uuid.create |> ignore)


[<Fact>]
let ``given a correct value for an Uuid when an Uuid try to create then an option with the Uuid is returned`` () =
    let value = "123e4567-e89b-12d3-a456-426655440000"
    let uuidOpt = value |> Uuid.tryCreate
    Assert.True(uuidOpt.IsSome)
    Assert.Equal(value, uuidOpt.Value |> Uuid.value)


[<Theory>]
[<MemberData("wrongValues")>]
let ``given a wrong value for an Uuid when an Uuid try to create then an option without value is returned`` (value: string) =
    let uuidOpt = value |> Uuid.tryCreate
    Assert.True(uuidOpt.IsNone)


[<Fact>]
let ``given a Guid when an Uuid is created from it then an Uuid is returned`` () =
    let guid = Guid.NewGuid()
    let uuid = guid |> Uuid.createFromGuid
    Assert.Equal(guid.ToString(), uuid |> Uuid.value)
    
    
[<Fact>]
let ``given an Uuid when it's converted to Guid then a Guid is returned`` () =
    let uuid = Uuid.newUuid ()
    let guid = uuid |> Uuid.toGuid
    Assert.Equal(uuid |> Uuid.value, guid.ToString())
