namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.Uuid

[<CLIMutable>]
type PoolDao =
    { PoolID: Uuid
      MasterPoolID: Uuid
      OwnerGamblerID: Uuid
      CreationDate: DateTime }

type PoolEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<PoolDao> with

        override this.Configure(builder: EntityTypeBuilder<PoolDao>) =
            builder.ToTable("POOL") |> ignore

            builder.HasKey(fun x -> x.PoolID :> obj) |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.OwnerGamblerID)
                .HasColumnName("OWNER_GAMBLER_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.CreationDate)
                .HasColumnName("CREATION_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore
