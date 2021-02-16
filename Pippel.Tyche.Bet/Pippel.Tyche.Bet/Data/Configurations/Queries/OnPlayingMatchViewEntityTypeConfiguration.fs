namespace Pippel.Tyche.Bet.Entities.Configurations.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models.Queries

type OnPlayingMatchViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<OnPlayingMatchViewDao> with

        override this.Configure(builder: EntityTypeBuilder<OnPlayingMatchViewDao>) =
            builder.ToView("V_ON_PLAYING_MATCH").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
            |> ignore
            
            builder
                .Property(fun x -> x.HomeTeamID)
                .HasColumnName("HOME_TEAM_ID")
            |> ignore
            
            builder
                .Property(fun x -> x.AwayTeamID)
                .HasColumnName("AWAY_TEAM_ID")
            |> ignore
            
            builder
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
            |> ignore
            
            builder
                .Property(fun x -> x.HomeTeamName)
                .HasColumnName("HOME_TEAM_NAME")
            |> ignore
            
            builder
                .Property(fun x -> x.AwayTeamName)
                .HasColumnName("AWAY_TEAM_NAME")
            |> ignore
            
            builder
                .Property(fun x -> x.GroupMatchID)
                .HasColumnName("GROUP_MATCH_ID")
            |> ignore
