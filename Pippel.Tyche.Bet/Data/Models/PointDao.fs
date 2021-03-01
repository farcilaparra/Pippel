namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

[<CLIMutable>]
type PointDao =
    { PointID: Uuid
      HomeResultPoint: PositiveInt
      AwayResultPoint: PositiveInt
      DiferencePoint: PositiveInt
      InvertedDiferentePoint: PositiveInt
      WinOrDrawPoint: PositiveInt }

type PointEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<PointDao> with

        override this.Configure(builder: EntityTypeBuilder<PointDao>) =
            builder.ToTable("POINT") |> ignore

            builder.HasKey(fun x -> x.PointID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.PointID)
                .HasColumnName("POINT_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.WinOrDrawPoint)
                .HasColumnName("WIN_OR_DRAW_POINT")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.HomeResultPoint)
                .HasColumnName("HOME_RESULT_POINT")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.AwayResultPoint)
                .HasColumnName("AWAY_RESULT_POINT")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.DiferencePoint)
                .HasColumnName("DIFERENCE_POINT")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.InvertedDiferentePoint)
                .HasColumnName("INVERTED_DIFERENCE_POINT")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore
