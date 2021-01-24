namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type HistoryBetEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<HistoryBetDao> with

        override this.Configure(builder: EntityTypeBuilder<HistoryBetDao>) =
            builder.ToTable("BET") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.ID)
                .HasColumnName("ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.BetID)
                .HasColumnName("BET_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.HomeTeamValue)
                .HasColumnName("HOME_TEAM_VALUE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.AwayTeamValue)
                .HasColumnName("AWAY_TEAM_VALUE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.CreationDate)
                .HasColumnName("CREATION_DATE")
                .IsRequired()
            |> ignore
