namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.Uuid

[<CLIMutable>]
type MasterPoolDao =
    { MasterPoolID: Uuid
      Name: NotEmptyString
      StartDate: DateTime
      EndDate: DateTime }

type MasterPoolEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MasterPoolDao> with

        override this.Configure(builder: EntityTypeBuilder<MasterPoolDao>) =
            builder.ToTable("MASTER_POOL") |> ignore

            builder.HasKey(fun x -> x.MasterPoolID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(NotEmptyStringValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.StartDate)
                .HasColumnName("START_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.EndDate)
                .HasColumnName("END_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore
