namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open System
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchGamblerViewMapper() =

        /// <summary>Maps from <c>MatchGamblerViewDao</c> to <c>MatchGamblerViewDto</c></summary>
        member this.MapToSource(matchGamblerViewDao: MatchGamblerViewDao): MatchGamblerViewDto =
            { MatchGamblerViewDto.GroupBetID = matchGamblerViewDao.GroupBetID
              GamblerID = matchGamblerViewDao.GamblerID
              GroupMatchName = matchGamblerViewDao.GroupMatchName
              CurrentPoint = matchGamblerViewDao.CurrentPoint
              CurrentPosition = matchGamblerViewDao.CurrentPosition
              BeforePosition = matchGamblerViewDao.BeforePosition }
