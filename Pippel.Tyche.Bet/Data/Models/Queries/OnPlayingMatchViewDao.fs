namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type OnPlayingMatchViewDao =
    { MatchID: Guid
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeTeamName: string
      AwayTeamName: string
      MasterPoolID: Guid }

type OnPlayingMatchViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<OnPlayingMatchViewDao> with

        override this.Configure(builder: EntityTypeBuilder<OnPlayingMatchViewDao>) =
            builder.ToView("V_ON_PLAYING_MATCH").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
                .IsRequired()
            |> ignore

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
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
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
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
            |> ignore
