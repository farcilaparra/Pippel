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
        { PoolDomain.ID = { PoolID = Uuid.From poolDao.PoolID }
          GroupMatchID = Uuid.From poolDao.MasterPoolID
          OwnerGamblerID = Uuid.From poolDao.OwnerGamblerID
          CreationDate = DateTime.From poolDao.CreationDate }
