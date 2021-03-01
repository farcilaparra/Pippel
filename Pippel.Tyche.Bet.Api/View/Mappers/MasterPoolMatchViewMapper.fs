namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Models.Queries

module MasterPoolMatchViewMapper =

    let mapToView (matchGroupViewDao: MasterPoolMatchViewDao) : MasterPoolMatchViewDto =
        { MasterPoolMatchViewDto.MasterPoolID = matchGroupViewDao.MasterPoolID
          HomeTeamName = matchGroupViewDao.HomeTeamName
          AwayTeamName = matchGroupViewDao.AwayTeamName
          MatchDate = matchGroupViewDao.MatchDate }
