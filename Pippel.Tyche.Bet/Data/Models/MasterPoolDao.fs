namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type MasterPoolDao =
    { MasterPoolID: Guid
      Name: string
      StartDate: DateTime
      EndDate: DateTime }

type MasterPoolEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<MasterPoolDao> with

        override this.Configure(builder: EntityTypeBuilder<MasterPoolDao>) =
            builder.ToTable("MASTER_POOL") |> ignore

            builder.HasKey(fun x -> x.MasterPoolID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
            |> ignore

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
