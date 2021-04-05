namespace Pippel.Tyche.Bet.Domain.Mappers

open System
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module PoolDomainMapper =

    let mapFromDomain (poolDomain: PoolDomain): PoolDao =
        { PoolDao.PoolID = poolDomain.ID.PoolID |> Uuid.value
          MasterPoolID = poolDomain.MasterPoolID |> Uuid.value
          OwnerGamblerID = poolDomain.OwnerGamblerID |> Uuid.value
          CreationDate = poolDomain.CreationDate |> DateTime.value
          Name = poolDomain.Name |> String.value }

    let mapToDomain (poolDao: PoolDao): PoolDomain =
        try
            { PoolDomain.ID = { PoolID = Uuid.From poolDao.PoolID }
              MasterPoolID = Uuid.From poolDao.MasterPoolID
              OwnerGamblerID = Uuid.From poolDao.OwnerGamblerID
              CreationDate = DateTime.From poolDao.CreationDate
              Name = NotEmptyString100.From poolDao.Name }
        with :? ArgumentException as ex -> raise <| DomainValueException(ex.Message)
