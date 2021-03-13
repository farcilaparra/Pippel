namespace Pippel.Tyche.Bet.Data.Models

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type PoolEnrollmentDao =
    { PoolID: Guid
      GamblerID: Guid
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
            |> ignore

            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.EnrollmentDate)
                .HasColumnName("ENROLLMENT_DATE")
                .IsRequired()
            |> ignore
