namespace Pippel.Core.Json

open System.Text.Json
open System.Text.Json.Serialization

type DefaultJsonSerializer() =

    /// Creates the default options for the serializer
    let CreateDefaultOptions () =
        let options =
            JsonSerializerOptions(PropertyNamingPolicy = JsonNamingPolicy.CamelCase)

        options.Converters.Add(JsonFSharpConverter())
        options

    interface IJsonSerializer with

        member this.Serialize<'T>(value: 'T): string =
            JsonSerializer.Serialize<'T>(value, CreateDefaultOptions())

        member this.Deserialize<'T>(value: string): 'T =
            JsonSerializer.Deserialize<'T>(value, CreateDefaultOptions())
