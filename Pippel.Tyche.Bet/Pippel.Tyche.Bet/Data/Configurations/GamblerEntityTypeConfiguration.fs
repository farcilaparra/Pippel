namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type GamblerEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GamblerDao> with

        override this.Configure(builder: EntityTypeBuilder<GamblerDao>) =
            builder.ToTable("GAMBLER") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.ID)
                .HasColumnName("ID")
                .IsRequired()
            |> ignore
