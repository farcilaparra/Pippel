namespace Pippel.Tyche.Bet.Data.Models.Queries

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Metadata.Builders

[<CLIMutable>]
type PoolReviewViewDao =
    { PoolID: Guid
      MasterPoolID: Guid
      GamblerID: Guid
      MasterPoolName: string
      StartDate: DateTime
      EndDate: DateTime
      CurrentPoint: int Nullable
      CurrentPosition: int Nullable
      BeforePosition: int Nullable }

type PoolReviewViewEntityTypeConfiguration() =
    interface IEntityTypeConfiguration<PoolReviewViewDao> with

        override this.Configure(builder: EntityTypeBuilder<PoolReviewViewDao>) =
            builder.ToView("V_POOL_REVIEW").HasNoKey()
            |> ignore

            builder
                .Property(fun x -> x.PoolID)
                .HasColumnName("POOL_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolID)
                .HasColumnName("MASTER_POOL_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.GamblerID)
                .HasColumnName("GAMBLER_ID")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.MasterPoolName)
                .HasColumnName("MASTER_POOL_NAME")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.StartDate)
                .HasColumnName("START_DATE")
            |> ignore

            builder
                .Property(fun x -> x.EndDate)
                .HasColumnName("END_DATE")
                .IsRequired()
            |> ignore

            builder
                .Property(fun x -> x.CurrentPoint)
                .HasColumnName("CURRENT_POINT")
            |> ignore

            builder
                .Property(fun x -> x.CurrentPosition)
                .HasColumnName("CURRENT_POSITION")
            |> ignore

            builder
                .Property(fun x -> x.BeforePosition)
                .HasColumnName("BEFORE_POSITION")
            |> ignore
