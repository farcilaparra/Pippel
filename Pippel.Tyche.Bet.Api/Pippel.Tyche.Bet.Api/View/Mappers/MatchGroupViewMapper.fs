namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

type MatchGroupViewMapper() =

    member this.Map(matchGroupViewDao: MatchGroupViewDao): MatchGroupViewDto =
        { MatchGroupViewDto.GroupMatchId = matchGroupViewDao.GroupMatchId
          HomeTeamName = matchGroupViewDao.HomeTeamName
          AwayTeamName = matchGroupViewDao.AwayTeamName
          MatchDate = matchGroupViewDao.MatchDate }
