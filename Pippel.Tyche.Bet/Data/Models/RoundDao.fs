namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type RoundDao =
    { RoundID: Guid
      MasterPoolID: Guid
      Name: string
      PointID: Guid }

type RoundEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<RoundDao> with

        override this.Configure(builder: EntityTypeBuilder<RoundDao>) =
            builder.ToTable("ROUND") |> ignore

            builder.HasKey(fun x -> x.RoundID :> obj)
            |> ignore

            builder
                .Property(fun x -> x.RoundID)
                .HasColumnName("ROUND_ID")
                .IsRequired()
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
                .HasMaxLength(30)
            |> ignore

            builder
                .Property(fun x -> x.PointID)
                .HasColumnName("POINT_ID")
                .IsRequired()
            |> ignore
