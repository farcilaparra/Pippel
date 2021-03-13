namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type BetDao =
    { PoolID: Guid
      GamblerID: Guid
      MatchID: Guid
      HomeTeamValue: int
      AwayTeamValue: int }

type BetEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<BetDao> with

        override this.Configure(builder: EntityTypeBuilder<BetDao>) =
            builder.ToTable("BET") |> ignore

            builder.HasKey(fun x -> (x.PoolID, x.GamblerID, x.MatchID) :> obj)
            |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
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
