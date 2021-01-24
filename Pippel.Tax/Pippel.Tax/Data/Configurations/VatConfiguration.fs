namespace Pippel.Tax.Entities.Configurations

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Tax.Data.Models

type VatEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<VatDao> with

        override this.Configure(builder: EntityTypeBuilder<VatDao>) =
            builder.ToTable("VAT") |> ignore
            builder.HasKey(fun x -> x.ID :> obj) |> ignore
            builder.Property(fun x -> x.ID)
                   .HasColumnName("ID")
                   .IsRequired() |> ignore
            builder.Property(fun x -> x.Name)
                   .HasColumnName("NAME")
                   .IsRequired()
                   .HasMaxLength(1000) |> ignore
            builder.Property(fun x -> x.Percentage)
                   .HasColumnName("PERCENTAGE")
                   .IsRequired() |> ignore
