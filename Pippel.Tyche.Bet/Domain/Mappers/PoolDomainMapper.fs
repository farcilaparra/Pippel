namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module PoolDomainMapper =

    let mapFromDomain (poolDomain: PoolDomain) : PoolDao =
        { PoolDao.PoolID = poolDomain.ID.PoolID
          MasterPoolID = poolDomain.GroupMatchID
          OwnerGamblerID = poolDomain.OwnerGamblerID
          CreationDate = poolDomain.CreationDate }

    let mapToDomain (poolDao: PoolDao) : PoolDomain =
        { PoolDomain.ID = { PoolID = poolDao.PoolID }
          GroupMatchID = poolDao.MasterPoolID
          OwnerGamblerID = poolDao.OwnerGamblerID
          CreationDate = poolDao.CreationDate }
