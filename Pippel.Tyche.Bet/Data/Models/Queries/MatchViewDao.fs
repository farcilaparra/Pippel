namespace Pippel.Tyche.Bet.Data.Models.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

[<CLIMutable>]
type MatchViewDao =
    { MatchID: Uuid
      RoundID: Uuid
      MasterPoolID: Uuid
      PoolID: Uuid
      MatchStatus: MatchStatus
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      MatchDate: DateTime
      HomeTeamName: NotEmptyString
      AwayTeamName: NotEmptyString
      Point: PositiveInt option }

type MatchViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchViewDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchViewDao>) =
            builder.ToView("V_MATCH").HasNoKey() |> ignore

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
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.MatchStatus)
                .HasColumnName("MATCH_STATUS")
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
                .Property(fun x -> x.Point)
                .HasColumnName("POINT")
                .HasConversion(PositiveIntOptionValueConverter())
            |> ignore
