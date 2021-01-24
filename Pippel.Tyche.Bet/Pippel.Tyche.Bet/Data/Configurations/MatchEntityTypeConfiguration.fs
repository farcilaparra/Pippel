namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type MatchEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchDao>) =
            builder.ToTable("MATCH") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.HomeTeamID)
                .HasColumnName("HOME_TEAM_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.AwayTeamID)
                .HasColumnName("AWAY_TEAM_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.RoundMatchID)
                .HasColumnName("ROUND_MATCH_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.HomeResult)
                .HasColumnName("HOME_RESULT")
            |> ignore

            builder
                .Property(fun x -> x.AwayResult)
                .HasColumnName("AWAY_RESULT")
            |> ignore

            builder
                .Property(fun x -> x.State)
                .HasColumnName("STATE")
                .IsRequired()
                .HasMaxLength(20)
            |> ignore
