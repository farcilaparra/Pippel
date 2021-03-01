namespace Pippel.Tyche.Bet.Data.Models.Queries

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders
open Pippel.Type
open Pippel.Type.DateTime
open Pippel.Type.NotEmptyString
open Pippel.Type.PositiveInt
open Pippel.Type.Uuid

[<CLIMutable>]
type PoolReviewViewDao =
    { PoolID: Uuid
      MasterPoolID: Uuid
      GamblerID: Uuid
      MasterPoolName: NotEmptyString
      StartDate: DateTime
      EndDate: DateTime
      CurrentPoint: PositiveInt
      CurrentPosition: PositiveInt
      BeforePosition: PositiveInt }

type PoolReviewViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<PoolReviewViewDao> with

        override this.Configure(builder: EntityTypeBuilder<PoolReviewViewDao>) =
            builder.ToView("V_POOL_REVIEW").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
                .IsRequired()
                .HasConversion(UuidValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
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
                .Property(fun x -> x.MasterPoolName)
                .HasColumnName("MASTER_POOL_NAME")
                .IsRequired()
                .HasConversion(NotEmptyStringValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.StartDate)
                .HasColumnName("START_DATE")
                .HasConversion(DateTimeValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.EndDate)
                .HasColumnName("END_DATE")
                .IsRequired()
                .HasConversion(DateTimeValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.CurrentPoint)
                .HasColumnName("CURRENT_POINT")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.CurrentPosition)
                .HasColumnName("CURRENT_POSITION")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore

            builder
                .Property(fun x -> x.BeforePosition)
                .HasColumnName("BEFORE_POSITION")
                .IsRequired()
                .HasConversion(PositiveIntValueConverter())
            |> ignore
