namespace Pippel.Tyche.Bet.Data.Models.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.Uuid

[<CLIMutable>]
type MasterPoolMatchViewDao =
    { MasterPoolID: Uuid
      HomeTeamName: NotEmptyString
      AwayTeamName: NotEmptyString
      MatchDate: DateTime }

type MasterPoolMatchViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MasterPoolMatchViewDao> with

        override this.Configure(builder: EntityTypeBuilder<MasterPoolMatchViewDao>) =
            builder.ToView("V_MASTER_POOL_MATCH") |> ignore

            builder.HasKey(fun x -> x.MasterPoolID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
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
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore
