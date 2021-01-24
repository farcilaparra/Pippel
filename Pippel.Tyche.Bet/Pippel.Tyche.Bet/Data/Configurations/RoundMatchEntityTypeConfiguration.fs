namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type RoundMatchEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<RoundMatchDao> with

        override this.Configure(builder: EntityTypeBuilder<RoundMatchDao>) =
            builder.ToTable("ROUND_MATCH") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.GroupMatchID)
                .HasColumnName("GROUP_MATCH_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(30)
            |> ignore
