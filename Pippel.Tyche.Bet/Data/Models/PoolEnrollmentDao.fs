namespace Pippel.Tyche.Bet.Data.Models

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.Uuid

[<CLIMutable>]
type PoolEnrollmentDao =
    { PoolID: Uuid
      GamblerID: Uuid
      EnrollmentDate: DateTime }

type PoolEnrollmentEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<PoolEnrollmentDao> with

        override this.Configure(builder: EntityTypeBuilder<PoolEnrollmentDao>) =
            builder.ToTable("POOL_ENROLLMENT") |> ignore

            builder.HasKey(fun x -> (x.PoolID, x.GamblerID) :> obj)
            |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.EnrollmentDate)
                .HasColumnName("ENROLLMENT_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore
