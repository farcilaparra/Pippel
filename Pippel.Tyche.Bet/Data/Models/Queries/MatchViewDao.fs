namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

[<CLIMutable>]
type MatchViewDao =
    { MatchID: Guid
      RoundID: Guid
      MasterPoolID: Guid
      PoolID: Guid
      MatchStatus: MatchStatus
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeTeamName: string
      AwayTeamName: string
      Point: int Nullable }

type MatchViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchViewDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchViewDao>) =
            builder.ToView("V_MATCH").HasNoKey() |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.RoundID)
                .HasColumnName("ROUND_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MatchStatus)
                .HasColumnName("MATCH_STATUS")
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
                .Property(fun x -> x.Point)
                .HasColumnName("POINT")
            |> ignore
