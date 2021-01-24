namespace Pippel.Tyche.Bet.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tyche.Bet.Data.Models

type AppUserEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<AppUserDao> with

        override this.Configure(builder: EntityTypeBuilder<AppUserDao>) =
            builder.ToTable("APP_USER") |> ignore

            builder.HasKey(fun x -> x.ID :> obj) |> ignore

            builder
                .Property(fun x -> x.ID)
                .HasColumnName("ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.Email)
                .HasColumnName("EMAIL")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.Password)
                .HasColumnName("PASSWORD")
                .IsRequired()
            |> ignore
