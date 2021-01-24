namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type RoundMatchDomainMapper() =
    interface IMapper<RoundMatch, RoundMatchDao> with

        /// <summary>Maps from <c>RoundMatch</c> to <c>RoundMatchDao</c></summary>
        member this.MapToTarget(roundMatch: RoundMatch): RoundMatchDao =
            { RoundMatchDao.ID = roundMatch.ID |> Uuid.toGuid
              GroupMatchID = roundMatch.GroupMatchID |> Uuid.toGuid
              Name = roundMatch.Name |> NonEmptyString.value }

        /// <summary>Maps from <c>RoundMatchDao</c> to <c>RoundMatch</c></summary>
        member this.MapToSource(roundMatchDao: RoundMatchDao): RoundMatch =
            { RoundMatch.ID = roundMatchDao.ID |> Uuid.createFromGuid
              GroupMatchID = roundMatchDao.GroupMatchID |> Uuid.createFromGuid
              Name = roundMatchDao.Name |> NonEmptyString.create }
