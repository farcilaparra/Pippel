namespace Pippel.Tyche.Bet.Data.Models.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

[<CLIMutable>]
type BetPositionViewDao =
    { PoolID: Uuid
      GamblerID: Uuid
      EnrollmentDate: DateTime
      Point: PositiveInt option
      CurrentPosition: PositiveInt option
      BeforePosition: PositiveInt option }

type BetPositionViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<BetPositionViewDao> with

        override this.Configure(builder: EntityTypeBuilder<BetPositionViewDao>) =
            builder.ToView("V_BET_POSITION").HasNoKey()
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

            builder
                .Property(fun x -> x.Point)
                .HasColumnName("POINT")
                .HasConversion(PositiveIntOptionValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.CurrentPosition)
                .HasColumnName("CURRENT_POSITION")
                .HasConversion(PositiveIntOptionValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.BeforePosition)
                .HasColumnName("BEFORE_POSITION")
                .HasConversion(PositiveIntOptionValueConverter())
            |> ignore
