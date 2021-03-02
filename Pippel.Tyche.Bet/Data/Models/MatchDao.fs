namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

type MatchStatus =
    | Pending = 0
    | Playing = 1
    | Played = 2
    | Suspended = 3
    | Canceled = 4

[<CLIMutable>]
type MatchDao =
    { MatchID: Uuid
      RoundID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt option
      AwayResult: PositiveInt option
      Status: MatchStatus }

type MatchEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchDao>) =
            builder.ToTable("MATCH") |> ignore

            builder.HasKey(fun x -> x.MatchID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.RoundID)
                .HasColumnName("ROUND_ID")
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
                .Property(fun x -> x.HomeResult)
                .HasColumnName("HOME_RESULT")
                .HasConversion(PositiveIntOptionValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.AwayResult)
                .HasColumnName("AWAY_RESULT")
                .HasConversion(PositiveIntOptionValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.Status)
                .HasColumnName("STATUS")
                .IsRequired()
            |> ignore
