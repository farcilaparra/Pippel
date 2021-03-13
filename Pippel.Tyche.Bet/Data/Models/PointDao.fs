namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type PointDao =
    { PointID: Guid
      HomeResultPoint: int
      AwayResultPoint: int
      DifferencePoint: int
      InvertedDifferencePoint: int
      WinOrDrawPoint: int }

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
            |> ignore

            builder
                .Property(fun x -> x.WinOrDrawPoint)
                .HasColumnName("WIN_OR_DRAW_POINT")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.HomeResultPoint)
                .HasColumnName("HOME_RESULT_POINT")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.AwayResultPoint)
                .HasColumnName("AWAY_RESULT_POINT")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.DifferencePoint)
                .HasColumnName("DIFERENCE_POINT")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.InvertedDifferencePoint)
                .HasColumnName("INVERTED_DIFERENCE_POINT")
                .IsRequired()
            |> ignore
