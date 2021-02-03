namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type GroupBetGamblerDomainMapper() =
    interface IMapper<GroupBetGambler, GroupBetGamblerDao> with

        /// <summary>Maps from <c>GroupBetGambler</c> to <c>GroupBetGamblerDao</c></summary>
        member this.MapToTarget(groupBetGambler: GroupBetGambler): GroupBetGamblerDao =
            { GroupBetGamblerDao.GroupBetID = groupBetGambler.GroupBetID |> Uuid.toGuid
              GroupBetGamblerDao.GamblerID = groupBetGambler.GamblerID |> Uuid.toGuid
              IsAdmin = groupBetGambler.IsAdmin |> NonEmptyString.value
              EnrollmentDate = groupBetGambler.EnrollmentDate
              CurrentPoint = groupBetGambler.CurrentPoint |> PositiveInt.value }


        /// <summary>Maps from <c>GroupBetGamblerDao</c> to <c>GroupBetGambler</c></summary>
        member this.MapToSource(groupBetGamblerDao: GroupBetGamblerDao): GroupBetGambler =
            { GroupBetGambler.GroupBetID = groupBetGamblerDao.GroupBetID |> Uuid.createFromGuid
              GroupBetGambler.GamblerID = groupBetGamblerDao.GamblerID |> Uuid.createFromGuid
              IsAdmin = groupBetGamblerDao.IsAdmin |> NonEmptyString.create
              EnrollmentDate = groupBetGamblerDao.EnrollmentDate
              CurrentPoint = groupBetGamblerDao.CurrentPoint |> PositiveInt.create }
