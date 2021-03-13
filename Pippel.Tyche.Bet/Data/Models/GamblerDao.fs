namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type GamblerDao = { UserID: Guid }

type GamblerEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<GamblerDao> with

        override this.Configure(builder: EntityTypeBuilder<GamblerDao>) =
            builder.ToTable("GAMBLER") |> ignore

            builder.HasKey(fun x -> x.UserID :> obj) |> ignore

            builder
                .Property(fun x -> x.UserID)
                .HasColumnName("USER_ID")
                .IsRequired()
            |> ignore
