namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type BetConfigEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<BetConfigDao> with

        override this.Configure(builder: EntityTypeBuilder<BetConfigDao>) =
            builder.ToTable("BET_CONFIG") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

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
                .Property(fun x -> x.DiferencePoint)
                .HasColumnName("DIFERENCE_POINT")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.InvertedDiferentePoint)
                .HasColumnName("INVERTED_DIFERENCE_POINT")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
                .IsRequired()
            |> ignore
