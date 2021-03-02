namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.Uuid

[<CLIMutable>]
type GamblerDao = { UserID: Uuid }

type GamblerEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GamblerDao> with

        override this.Configure(builder: EntityTypeBuilder<GamblerDao>) =
            builder.ToTable("GAMBLER") |> ignore

            builder.HasKey(fun x -> x.UserID :> obj) |> ignore

            builder
                .Property(fun x -> x.UserID)
                .HasColumnName("USER_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore
