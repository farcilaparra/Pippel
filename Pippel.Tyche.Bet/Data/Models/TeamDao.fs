namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type TeamDao =
    { TeamID: Guid
      Name: string }

type TeamEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<TeamDao> with

        override this.Configure(builder: EntityTypeBuilder<TeamDao>) =
            builder.ToTable("TEAM") |> ignore

            builder.HasKey(fun x -> x.TeamID :> obj) |> ignore

            builder
                .Property(fun x -> x.TeamID)
                .HasColumnName("TEAM_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(100)
            |> ignore
