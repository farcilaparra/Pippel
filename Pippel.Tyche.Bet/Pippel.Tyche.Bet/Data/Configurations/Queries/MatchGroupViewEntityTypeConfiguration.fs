namespace Pippel.Tyche.Bet.Entities.Configurations.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchGroupViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchGroupViewDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchGroupViewDao>) =
            builder.ToView("V_MATCH_GROUP") |> ignore

            builder.HasKey(fun x -> x.GroupMatchId :> obj) |> ignore

            builder
                .Property(fun x -> x.GroupMatchId)
                .HasColumnName("GROUP_MATCH_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.HomeTeamName)
                .HasColumnName("HOME_TEAM_NAME")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.AwayTeamName)
                .HasColumnName("AWAY_TEAM_NAME")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
                .IsRequired()
            |> ignore