namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.NotEmptyString
open Pippel.Type.Uuid

[<CLIMutable>]
type RoundDao =
    { RoundID: Uuid
      MasterPoolID: Uuid
      Name: NotEmptyString
      PointID: Uuid }

type RoundEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<RoundDao> with

        override this.Configure(builder: EntityTypeBuilder<RoundDao>) =
            builder.ToTable("ROUND") |> ignore

            builder.HasKey(fun x -> x.RoundID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.RoundID)
                .HasColumnName("ROUND_ID")
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
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion(NotEmptyStringValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.PointID)
                .HasColumnName("POINT_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore
