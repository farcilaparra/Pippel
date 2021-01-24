namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type GroupBetEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GroupBetDao> with

        override this.Configure(builder: EntityTypeBuilder<GroupBetDao>) =
            builder.ToTable("GROUP_BET") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.ID)
                .HasColumnName("ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.GroupMatchID)
                .HasColumnName("GROUP_MATCH_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.OwnerGamblerID)
                .HasColumnName("OWNER_GAMBLER_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.CreationDate)
                .HasColumnName("CREATION_DATE")
                .IsRequired()
            |> ignore
