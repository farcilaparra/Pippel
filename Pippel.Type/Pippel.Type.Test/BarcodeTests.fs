module BarcodeTests

open System
open Xunit
open Pippel.Type

let barcodes : obj [] seq =
    seq {
        yield [| "90311017" |] // GTIN-8
        yield [| "012345678905" |] // GTIN-12
        yield [| "9780201379624" |] // GTIN-13
        yield [| "40700719670720" |] // GTIN 14
    }

let wrongValues : obj [] seq =
    seq {
        yield [| null |]
        yield [| "hello" |]
    }

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a correct value for a barcode when a Barcode is created then a Barcode is returned`` (value: string) =
    let barcode = value |> Barcode.from
    Assert.True(true)
    Assert.Equal(value, barcode |> Barcode.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a barcode when a Barcode is created then an exception is raised`` (value: string) =
    Assert.Throws<ArgumentException>(fun () -> value |> Barcode.from |> ignore)

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a correct value for a barcode when a Barcode tries to create then an option with the barcode is returned``
    (value: string)
    =
    let barcodeOpt = value |> Barcode.tryFrom
    Assert.True(barcodeOpt.IsSome)
    Assert.Equal(value, barcodeOpt.Value |> Barcode.value)

[<Theory>]
[<MemberData(nameof (wrongValues))>]
let ``given a wrong value for a barcode when a Barcode tries to create then an option without value is returned``
    (value: string)
    =
    let barcodeOpt = value |> Barcode.tryFrom
    Assert.True(barcodeOpt.IsNone)

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a barcode string when it's converted to Barcode option then a barcode option with value is returned``
    (value: string)
    =
    let provider = value

    let model =
        Barcode.EntityFrameworkCore.toOption provider

    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> Barcode.value)

[<Fact>]
let ``given a null string when it's converted to Barcode option then a Barcode option with none is returned`` () =
    let provider : string = null

    let model =
        Barcode.EntityFrameworkCore.toOption provider

    Assert.True(model.IsNone)

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a Barcode option with value when it's converted to string then an string with value is returned``
    (value: string)
    =
    let model = Some(value |> Barcode.from)

    let provider =
        Barcode.EntityFrameworkCore.fromOption model

    Assert.True(not (isNull provider))
    Assert.Equal(model.Value |> Barcode.value, provider)

[<Fact>]
let ``given a Barcode option with none when an it's converted to string then an string with null is returned`` () =
    let model : Barcode option = None

    let provider =
        Barcode.EntityFrameworkCore.fromOption model

    Assert.True(isNull provider)

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a barcode string when it's converted to Barcode using model converter then a Barcode is returned``
    (provider: string)
    =
    let model = Barcode.Model.toModel provider
    Assert.Equal(provider, model |> Barcode.toString)

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a barcode string when it tries to convert to Barcode using model converter then a Barcode option with value is returned``
    (provider: string)
    =
    let model = Barcode.Model.tryToModel provider
    Assert.True(model.IsSome)
    Assert.Equal(provider, model.Value |> Barcode.toString)

[<Fact>]
let ``given an invalid barcode string when it tries to convert to Barcode using model converter then a Barcode option without value is returned``
    ()
    =
    let provider = "hello"
    let model = Barcode.Model.tryToModel provider
    Assert.True(model.IsNone)

[<Theory>]
[<MemberData(nameof (barcodes))>]
let ``given a Barcode when it's converted to barcode string using model converter then an barcode string is returned``
    (value: string)
    =
    let model = value |> Barcode.from
    let provider = Barcode.Model.fromModel model
    Assert.Equal(model |> Barcode.toString, provider)
