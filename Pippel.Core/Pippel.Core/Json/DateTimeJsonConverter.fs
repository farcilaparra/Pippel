namespace Pippel.Core.Json

open System
open System.Text.Json
open System.Text.Json.Serialization

type DateTimeJsonConverter() =
    inherit JsonConverter<DateTime>()

    override this.Read(reader: byref<Utf8JsonReader>, typeToConvert: Type, options: JsonSerializerOptions): DateTime =
        DateTime.Parse(reader.GetString())


    override this.Write(writer: Utf8JsonWriter, value: DateTime, options: JsonSerializerOptions) =
        writer.WriteStringValue(value.ToUniversalTime().ToString("o"))
        |> ignore
