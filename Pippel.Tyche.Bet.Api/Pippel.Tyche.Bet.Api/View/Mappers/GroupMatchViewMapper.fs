namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

type GroupMatchViewMapper() =
    interface IMapper<GroupMatchDto, GroupMatch> with

        /// <summary>Maps from <c>GroupMatchDto</c> to <c>GroupMatch</c></summary>
        member this.MapToTarget(groupMatchDto: GroupMatchDto): GroupMatch =
            { GroupMatch.ID = groupMatchDto.ID |> Uuid.createFromGuid
              Name = groupMatchDto.Name |> NonEmptyString.create
              StartDate = groupMatchDto.StartDate
              EndDate = groupMatchDto.EndDate }


        /// <summary>Maps from <c>GroupMatch</c> to <c>BetMatch</c></summary>
        member this.MapToSource(bet: GroupMatch): GroupMatchDto =
            { GroupMatchDto.ID = bet.ID |> Uuid.toGuid
              Name = bet.Name |> NonEmptyString.value
              StartDate = bet.StartDate
              EndDate = bet.EndDate }
