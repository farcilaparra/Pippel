# Common types for F#

This project has the next types:

- `Barcode`
- `DateTime`
- `NotEmptyString`
- `Percentage`
- `PositiveInt`
- `Uuid`

The next one is an example of a model that uses this data types:

```f#
type Vat =
    { ID: Uuid
      Name: NonEmptyString
      Percentage: Percentage }
```

the next one is an example for using

```f#
let uuid = Guid("9798ff07-f9d5-462d-acb1-bd58b553ff2e") |> Uuid.from 
```

## `Barcode`

Data type to manage barcodes. The supported formats are: GTIN-8, GTIN-12, GTIN-13, GTIN-14. It has the next functions:

- `tryFrom`: Returns a Barcode option.
- `from`: Creates a Barcode. Raise an exception if it hasn't a valid format.
- `value`: Returns the value of Barcode as a string.
- `toString`: Returns the Barcode string.

## `DateTime`

Data type to manage date and time. It has the next functions:

- `from`: Creates an DateTime. Raise an exception if it hasn't a valid format.
- `fromString`: Creates an DateTime from a string. Raise an exception if it hasn't a valid format.
- `value`: Returns the DateTime's value as a string.
- `now`: Gets current date and time.
- `toString`: Returns the DateTime string.
- `toStringWithFormat`: Returns the DateTime string applying a format.
- `toUniversalTime`: Returns the utc time.

## `NotEmptyString`

Data type to manage strings that don't let null or non empty values. It has the next functions:

- `tryFrom`: Returns a NonEmptyString option.
- `from`: Creates a NonEmptyString. Raise an exception if it hasn't a valid format.
- `value`: Returns the NonEmptyString's value as a string.
- `toString`: Returns the NonEmptyString string.

## `Percentage`

Data type to manage numbers between 0 (0%) and 1 (100%). It has the next functions:

- `tryFrom`: Returns a Percentage option.
- `from`: Creates a Percentage. Raise an exception if it hasn't a valid format.
- `value`: Returns the Percentage's value as a number.
- `toString`: Returns the Percentage string.

## `PositiveInt`

Data type to manage numbers greate or equal that 0. It has the next functions:

- `tryFrom`: Returns a PositiveInt option.
- `from`: Creates a PositiveInt. Raise an exception if it hasn't a valid format.
- `value`: Returns the PositiveInt's value as a number.
- `toString`: Returns the PositiveInt string.

## `Uuid`

Data type to manage Universally Unique Identifier. It has the next functions:

- `from`: Creates an Uuid. Raise an exception if it hasn't a valid format.
- `fromString`: Creates an Uuid from a string. Raise an exception if it hasn't a valid format.
- `value`: Returns the Uuid`s value as a string.
- `newUuid`: Creates a random Uuid.
- `toString`: Returns the Uuid string.

## Type Converter

You must add this line in startup.fs `Uuid.initTypeConverter()`:

## JSON

This types can use as in-out values of web api. For use them you must add a conversion.

```f#
member this.ConfigureServices(services: IServiceCollection) =
    services
        .AddControllers()
        .AddJsonOptions(fun options ->
            options.JsonSerializerOptions.Converters.Add(UuidJsonConverter()))
    |> ignore
```

**_NOTE:_** You must add this line `.AddJsonOptions(fun options -> options.JsonSerializerOptions.Converters.Add(UuidJsonConverter()))`


## Entity Framework Core

This types can use with entity framework core models. For use them you must add a conversion to each property.

```f#
type GamblerEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GamblerDao> with

        override this.Configure(builder: EntityTypeBuilder<GamblerDao>) =
            builder.ToTable("GAMBLER") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.ID)
                .HasColumnName("USER_ID")
                .HasConversion(UuidValueConverter())
                .IsRequired()
            |> ignore 
```

**_NOTE:_** You must add this line `HasConversion(UuidValueConverter())`


