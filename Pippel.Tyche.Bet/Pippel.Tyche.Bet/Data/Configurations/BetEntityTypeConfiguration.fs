namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type BetEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<BetDao> with

        override this.Configure(builder: EntityTypeBuilder<BetDao>) =
            builder.ToTable("BET") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.ID)
                .HasColumnName("ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.GroupBetID)
                .HasColumnName("GROUP_BET_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
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
                .Property(fun x -> x.LastPosition)
                .HasColumnName("LAST_POSITION")
                .IsRequired()
            |> ignore
