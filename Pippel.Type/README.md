# Common types for F#

This project has the next types:
- Barcode
- NonEmptyString
- Percentage
- PositiveInt32
- Uuid

The next one is an example of a model that uses this data types:

```f#
type Vat =
    { ID: Uuid
      Name: NonEmptyString
      Percentage: Percentage }
```

the next one is an example for using

```f#
let uuid = "9798ff07-f9d5-462d-acb1-bd58b553ff2e" |> Uuid.create 
```
  
## Barcode
Data type to manage barcodes. The supported formats are: GTIN-8, GTIN-12, GTIN-13, GTIN-14. It has the next functions:
- **tryCreate**: Returns a Barcode option.
- **create**: Creates a Barcode. Raise an exception if it hasn't a valid format.
- **value**: Returns the value of Barcode as a string.

## NonEmptyString
Data type to manage strings that don´t let null or non empty values. It has the next functions:
- **tryCreate**: Returns a NonEmptyString option.
- **create**: Creates a NonEmptyString. Raise an exception if it hasn't a valid format.
- **value**: Returns the NonEmptyString's value as a string.

## Percentage
Data type to manage numbers between 0 (0%) and 1 (100%). It has the next functions:
- **tryCreate**: Returns a Percentage option.
- **create**: Creates a Percentage. Raise an exception if it hasn't a valid format.
- **value**: Returns the Percentage's value as a number.

## PositiveInt32
Data type to manage numbers greate or equal that 0. It has the next functions:
- **tryCreate**: Returns a PositiveInt32 option.
- **create**: Creates a PositiveInt32. Raise an exception if it hasn't a valid format.
- **value**: Returns the PositiveInt32's value as a number.

## Uuid
Data type to manage Universally Unique Identifier. It has the next functions:
- **tryCreate**: Returns an Uuid option.
- **create**: Creates an Uuid. Raise an exception if it hasn't a valid format.
- **value**: Returns the Uuid`s value as a string.
- **newUuid**: Creates a random Uuid.
