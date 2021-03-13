namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

type MatchStatus =
    | Pending = 0
    | Playing = 1
    | Played = 2
    | Suspended = 3
    | Canceled = 4

[<CLIMutable>]
type MatchDao =
    { MatchID: Guid
      RoundID: Guid
      HomeTeamID: Guid
      AwayTeamID: Guid
      MatchDate: DateTime
      HomeResult: int Nullable
      AwayResult: int Nullable
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
            |> ignore

            builder
                .Property(fun x -> x.RoundID)
                .HasColumnName("ROUND_ID")
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
                .Property(fun x -> x.HomeResult)
                .HasColumnName("HOME_RESULT")
            |> ignore

            builder
                .Property(fun x -> x.AwayResult)
                .HasColumnName("AWAY_RESULT")
            |> ignore

            builder
                .Property(fun x -> x.Status)
                .HasColumnName("STATUS")
                .IsRequired()
            |> ignore
