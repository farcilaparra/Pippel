namespace Pippel.Tyche.Bet.Data.Models.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.Uuid

[<CLIMutable>]
type OnPlayingMatchViewDao =
    { MatchID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      MatchDate: DateTime
      HomeTeamName: NotEmptyString
      AwayTeamName: NotEmptyString
      MasterPoolID: Uuid }

type OnPlayingMatchViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<OnPlayingMatchViewDao> with

        override this.Configure(builder: EntityTypeBuilder<OnPlayingMatchViewDao>) =
            builder.ToView("V_ON_PLAYING_MATCH").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.HomeTeamID)
                .HasColumnName("HOME_TEAM_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.AwayTeamID)
                .HasColumnName("AWAY_TEAM_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.HomeTeamName)
                .HasColumnName("HOME_TEAM_NAME")
                .IsRequired()
                .HasConversion(NotEmptyStringValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.AwayTeamName)
                .HasColumnName("AWAY_TEAM_NAME")
                .IsRequired()
                .HasConversion(NotEmptyStringValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore
