namespace Pippel.Tyche.Bet.Entities.Configurations.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models.Queries

type BetPositionViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<BetPositionViewDao> with

        override this.Configure(builder: EntityTypeBuilder<BetPositionViewDao>) =
            builder.ToView("V_BET_POSITION").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.GroupBetID)
                .HasColumnName("GROUP_BET_ID")
            |> ignore
            
            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
            |> ignore
            
            builder
                .Property(fun x -> x.EnrollmentDate)
                .HasColumnName("ENROLLMENT_DATE")
            |> ignore
            
            builder
                .Property(fun x -> x.Point)
                .HasColumnName("POINT")
            |> ignore
            
            builder
                .Property(fun x -> x.CurrentPosition)
                .HasColumnName("CURRENT_POSITION")
            |> ignore
            
            builder
                .Property(fun x -> x.BeforePosition)
                .HasColumnName("BEFORE_POSITION")
            |> ignore
