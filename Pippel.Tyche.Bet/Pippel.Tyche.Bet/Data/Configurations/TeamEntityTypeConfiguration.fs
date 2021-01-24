namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type TeamEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<TeamDao> with

        override this.Configure(builder: EntityTypeBuilder<TeamDao>) =
            builder.ToTable("TEAM") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.TeamName)
                .HasColumnName("TEAM_NAME")
                .IsRequired()
                .HasMaxLength(30)
            |> ignore
