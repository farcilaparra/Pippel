namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type GroupMatchEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GroupMatchDao> with

        override this.Configure(builder: EntityTypeBuilder<GroupMatchDao>) =
            builder.ToTable("GROUP_MATCH") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100)
            |> ignore

            builder
                .Property(fun x -> x.StartDate)
                .HasColumnName("START_DATE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.EndDate)
                .HasColumnName("END_DATE")
                .IsRequired()
            |> ignore
