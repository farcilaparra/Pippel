namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

[<CLIMutable>]
type BetDao =
    { PoolID: Uuid
      GamblerID: Uuid
      MatchID: Uuid
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt }

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
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.MatchID)
                .HasColumnName("MATCH_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.HomeTeamValue)
                .HasColumnName("HOME_TEAM_VALUE")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.AwayTeamValue)
                .HasColumnName("AWAY_TEAM_VALUE")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore
