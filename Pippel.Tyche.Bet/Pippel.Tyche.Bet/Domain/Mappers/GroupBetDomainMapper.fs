namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type GroupBetDomainMapper() =
    interface IMapper<GroupBet, GroupBetDao> with

        /// <summary>Maps from <c>GroupBet</c> to <c>GroupBetDao</c></summary>
        member this.MapToTarget(groupBet: GroupBet): GroupBetDao =
            { GroupBetDao.ID = groupBet.ID |> Uuid.toGuid
              GroupMatchID = groupBet.GroupMatchID |> Uuid.toGuid
              OwnerGamblerID = groupBet.OwnerGamblerID |> Uuid.toGuid
              CreationDate = groupBet.CreationDate }


        /// <summary>Maps from <c>GroupBetDao</c> to <c>GroupBet</c></summary>
        member this.MapToSource(groupBetDao: GroupBetDao): GroupBet =
            { GroupBet.ID = groupBetDao.ID |> Uuid.createFromGuid
              GroupMatchID = groupBetDao.GroupMatchID |> Uuid.createFromGuid
              OwnerGamblerID = groupBetDao.OwnerGamblerID |> Uuid.createFromGuid
              CreationDate = groupBetDao.CreationDate }
