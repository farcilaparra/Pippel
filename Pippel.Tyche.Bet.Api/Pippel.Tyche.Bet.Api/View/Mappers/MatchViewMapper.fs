namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchViewMapper() =

        /// <summary>Maps from <c>MatchViewDao</c> to <c>MatchViewDto</c></summary>
        member this.MapToSource(matchViewDao: MatchViewDao): MatchViewDto =
            { MatchID = matchViewDao.MatchID
              RoundMatchID = matchViewDao.RoundMatchID
              GroupMatchID = matchViewDao.GroupMatchID
              GroupBetID = matchViewDao.GroupBetID
              MatchStatus = matchViewDao.MatchStatus
              HomeTeamID = matchViewDao.HomeTeamID
              AwayTeamID = matchViewDao.AwayTeamID
              MatchDate = matchViewDao.MatchDate
              HomeTeamName = matchViewDao.HomeTeamName
              AwayTeamName = matchViewDao.AwayTeamName
              Point = matchViewDao.Point }
