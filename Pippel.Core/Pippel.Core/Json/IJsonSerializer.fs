namespace Pippel.Core.Json

[<Interface>]
type IJsonSerializer =

    /// Converts the value of a type specified by a generic type parameter into a JSON string
    abstract Serialize<'T> : 'T -> string
    
    /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter
    abstract Deserialize<'T> : string -> 'T
