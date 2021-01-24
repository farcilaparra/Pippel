# Project that manage the domain of taxes

This project was made for manage the domain and the persistence of taxes. The persistence was made using Entity Framework Core.

## IVatRepository

It's an interface with the actions over the vat.

```f#
[<Interface>]
type IVatRepository =
    inherit IRepository<VatDao>
```

## VatRepository

It's an implementation of `IVatRepository`.

```f#
type VatRepositoryInDB(context: TaxContext) =
    inherit Repository<VatDao>(context)
    interface IVatRepository
```