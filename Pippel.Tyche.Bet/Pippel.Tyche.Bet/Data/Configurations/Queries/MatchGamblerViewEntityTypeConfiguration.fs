namespace Pippel.Tyche.Bet.Entities.Configurations.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchGamblerViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MatchGamblerViewDao> with

        override this.Configure(builder: EntityTypeBuilder<MatchGamblerViewDao>) =
            builder.ToView("V_MATCH_GROUP_GAMBLER").HasNoKey()
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
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
            |> ignore

            builder
                .Property(fun x -> x.GroupMatchName)
                .HasColumnName("GROUP_MATCH_NAME")
            |> ignore

            builder
                .Property(fun x -> x.StartDate)
                .HasColumnName("START_DATE")
            |> ignore

            builder
                .Property(fun x -> x.EndDate)
                .HasColumnName("END_DATE")
            |> ignore
            
            builder
                .Property(fun x -> x.CurrentPoint)
                .HasColumnName("CURRENT_POINT")
            |> ignore
            
            builder
                .Property(fun x -> x.CurrentPosition)
                .HasColumnName("CURRENT_POSITION")
            |> ignore
            
            builder
                .Property(fun x -> x.BeforePosition)
                .HasColumnName("BEFORE_POSITION")
            |> ignore
