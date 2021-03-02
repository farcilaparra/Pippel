module EntityFrameworkCoreTests

open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Pippel.Type
open Pippel.Type.Barcode
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.Percentage
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid
open Xunit

[<CLIMutable>]
type Item =
    { ID: string
      UuidProperty: Uuid
      UuidOptionProperty: Uuid option
      BarcodePropery: Barcode
      BarcodeOptionProperty: Barcode option
      NotEmptyStringProperty: NotEmptyString
      NotEmptyStringOptionProperty: NotEmptyString option
      PercentageProperty: Percentage
      PercentageOptionProperty: Percentage option
      PositiveIntProperty: PositiveInt
      PositiveIntOptionProperty: PositiveInt option
      DateTimeProperty: DateTime
      DateTimeOptionProperty: DateTime option }

type Context(options: DbContextOptions<Context>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private items: DbSet<Item>

    member this.Items
        with get () = this.items
        and set v = this.items <- v

    override this.OnModelCreating builder =
        builder
            .Entity<Item>()
            .Property(fun property -> property.UuidProperty)
            .HasConversion(UuidValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.UuidOptionProperty)
            .HasConversion(UuidOptionValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.BarcodePropery)
            .HasConversion(BarcodeValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.BarcodeOptionProperty)
            .HasConversion(BarcodeOptionValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.NotEmptyStringProperty)
            .HasConversion(NotEmptyStringValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.NotEmptyStringOptionProperty)
            .HasConversion(NotEmptyStringOptionValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.PercentageProperty)
            .HasConversion(PercentageValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.PercentageOptionProperty)
            .HasConversion(PercentageOptionValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.PositiveIntProperty)
            .HasConversion(PositiveIntValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.PositiveIntOptionProperty)
            .HasConversion(PositiveIntOptionValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.DateTimeProperty)
            .HasConversion(DateTimeValueConverter())
        |> ignore

        builder
            .Entity<Item>()
            .Property(fun property -> property.DateTimeOptionProperty)
            .HasConversion(DateTimeOptionValueConverter())
        |> ignore

let createContext () =
    new Context(
        DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(
            Guid.NewGuid().ToString()
        )
            .Options
    )

let createData () =
    [| { Item.ID = "item1"
         UuidProperty = newUuid ()
         UuidOptionProperty = None
         BarcodePropery = "90311017" |> Barcode.from
         BarcodeOptionProperty = None
         NotEmptyStringProperty = "hello" |> NotEmptyString.from
         NotEmptyStringOptionProperty = None
         PercentageProperty = 1.0 |> Percentage.from
         PercentageOptionProperty = None
         PositiveIntProperty = 1 |> PositiveInt.from
         PositiveIntOptionProperty = None
         DateTimeProperty = DateTime.now
         DateTimeOptionProperty = None } |]

let createContextWithData (items: Item []) =
    let context = createContext ()

    context.Items.AddRange items
    context.SaveChanges() |> ignore

    context

[<Fact>]
let ``given a non nullable primitive type when a type converter is added to entity framework then the type converter is added``
    ()
    =
    let items = createData ()
    let context = createContextWithData items
    let foundItems = context.Items.ToArray()
    Assert.Equal(items.Count(), foundItems.Count())
