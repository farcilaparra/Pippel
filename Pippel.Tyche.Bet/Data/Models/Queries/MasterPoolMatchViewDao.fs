namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type MasterPoolMatchViewDao =
    { MasterPoolID: Guid
      HomeTeamName: string
      AwayTeamName: string
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
                .Property(fun x -> x.MatchDate)
                .HasColumnName("MATCH_DATE")
                .IsRequired()
            |> ignore
