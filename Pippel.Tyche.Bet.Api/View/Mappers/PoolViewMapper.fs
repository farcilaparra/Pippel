namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open System
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

module PoolViewMapper =

    let mapFromView (poolDto: PoolDto): PoolDomain =
        { PoolDomain.ID =
              { PoolID =
                    match poolDto.PoolID.HasValue with
                    | true -> Uuid.FromNullable poolDto.PoolID
                    | false -> Uuid.newUuid () }
          MasterPoolID = Uuid.From poolDto.MasterPoolID
          OwnerGamblerID = Uuid.From poolDto.OwnerGamblerID
          CreationDate = DateTime.now ()
          Name =
              NotEmptyString100.From
                  (match poolDto.Name with
                   | null -> String.Empty
                   | name -> name.Trim()) }

    let mapToView (poolDomain: PoolDomain): PoolDto =
        { PoolDto.PoolID = Nullable(poolDomain.ID.PoolID |> Uuid.value)
          MasterPoolID = poolDomain.MasterPoolID |> Uuid.value
          OwnerGamblerID = poolDomain.OwnerGamblerID |> Uuid.value
          Name = poolDomain.Name |> String.value }
