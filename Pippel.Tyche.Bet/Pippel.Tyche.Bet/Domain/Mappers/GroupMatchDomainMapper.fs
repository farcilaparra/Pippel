namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type GroupMatchDomainMapper() =
    interface IMapper<GroupMatch, GroupMatchDao> with

        /// <summary>Maps from <c>GroupMatch</c> to <c>GroupMatchDao</c></summary>
        member this.MapToTarget(groupMatch: GroupMatch): GroupMatchDao =
            { GroupMatchDao.ID = groupMatch.ID |> Uuid.toGuid
              Name = groupMatch.Name |> NonEmptyString.value
              StartDate = groupMatch.StartDate
              EndDate = groupMatch.EndDate }

        /// <summary>Maps from <c>GroupMatchDao</c> to <c>GroupMatch</c></summary>
        member this.MapToSource(groupMatchDao: GroupMatchDao): GroupMatch =
            { GroupMatch.ID = groupMatchDao.ID |> Uuid.createFromGuid
              Name = groupMatchDao.Name |> NonEmptyString.create
              StartDate = groupMatchDao.StartDate
              EndDate = groupMatchDao.EndDate }
