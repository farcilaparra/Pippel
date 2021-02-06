namespace Pippel.Tyche.Bet.Entities.Configurations.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchGamblerViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchGamblerViewDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchGamblerViewDao>) =
            builder.ToView("V_GAMBLER_BET").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.GroupBetID)
                .HasColumnName("GROUP_BET_ID")
            |> ignore

            builder
                .Property(fun x -> x.GroupMatchID)
                .HasColumnName("GROUP_MATCH_ID")
            |> ignore

            builder
                .Property(fun x -> x.OwnerGamblerID)
                .HasColumnName("OWNER_GAMBLER_ID")
            |> ignore

            builder
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
            |> ignore

            builder
                .Property(fun x -> x.StartDate)
                .HasColumnName("START_DATE")
            |> ignore

            builder
                .Property(fun x -> x.EndDate)
                .HasColumnName("END_DATE")
            |> ignore
