namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.NotEmptyString
open Pippel.Type.Uuid

[<CLIMutable>]
type TeamDao =
    { TeamID: Uuid
      Name: NotEmptyString }

type TeamEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<TeamDao> with

        override this.Configure(builder: EntityTypeBuilder<TeamDao>) =
            builder.ToTable("TEAM") |> ignore

            builder.HasKey(fun x -> x.TeamID :> obj) |> ignore

            builder
                .Property(fun x -> x.TeamID)
                .HasColumnName("TEAM_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(30)
                .HasConversion(NotEmptyStringValueConverter())
            |> ignore
