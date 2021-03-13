namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type BetPositionViewDao =
    { PoolID: Guid
      GamblerID: Guid
      EnrollmentDate: DateTime
      Point: int Nullable
      CurrentPosition: int Nullable
      BeforePosition: int Nullable }

type BetPositionViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<BetPositionViewDao> with

        override this.Configure(builder: EntityTypeBuilder<BetPositionViewDao>) =
            builder.ToView("V_BET_POSITION").HasNoKey()
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

            builder
                .Property(fun x -> x.Point)
                .HasColumnName("POINT")
            |> ignore

            builder
                .Property(fun x -> x.CurrentPosition)
                .HasColumnName("CURRENT_POSITION")
            |> ignore

            builder
                .Property(fun x -> x.BeforePosition)
                .HasColumnName("BEFORE_POSITION")
            |> ignore
