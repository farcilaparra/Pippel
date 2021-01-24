namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type GroupBetGamblerEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GroupBetGamblerDao> with

        override this.Configure(builder: EntityTypeBuilder<GroupBetGamblerDao>) =
            builder.ToTable("GROUP_BET") |> ignore

            builder.HasKey(fun x -> (x.GroupBetID, x.GamblerID) :> obj)
            |> ignore

            builder
                .Property(fun x -> x.GroupBetID)
                .HasColumnName("GROUP_BET_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.IsAdmin)
                .HasColumnName("ROLE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.EnrollmentDate)
                .HasColumnName("ENROLLMENT_DATE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.CurrentPoint)
                .HasColumnName("CURRENT_POINT")
                .IsRequired()
            |> ignore
