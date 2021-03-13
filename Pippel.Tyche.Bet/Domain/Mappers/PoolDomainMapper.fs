namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module PoolDomainMapper =

    let mapFromDomain (poolDomain: PoolDomain) : PoolDao =
        { PoolDao.PoolID = poolDomain.ID.PoolID |> Uuid.value
          MasterPoolID = poolDomain.GroupMatchID |> Uuid.value
          OwnerGamblerID = poolDomain.OwnerGamblerID |> Uuid.value
          CreationDate = poolDomain.CreationDate |> DateTime.value }

    let mapToDomain (poolDao: PoolDao) : PoolDomain =
        { PoolDomain.ID = { PoolID = poolDao.PoolID |> Uuid.from }
          GroupMatchID = poolDao.MasterPoolID |> Uuid.from
          OwnerGamblerID = poolDao.OwnerGamblerID |> Uuid.from
          CreationDate = poolDao.CreationDate |> DateTime.from }
