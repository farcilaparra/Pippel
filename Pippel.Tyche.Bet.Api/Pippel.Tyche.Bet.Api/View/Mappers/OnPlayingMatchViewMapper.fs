namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Data.Models.Queries

type OnPlayingMatchViewMapper() =

    member this.Map(onPlayingMatchViewDao: OnPlayingMatchViewDao): OnPlayingMatchViewDto =
        { OnPlayingMatchViewDto.MatchID = onPlayingMatchViewDao.MatchID
          HomeTeamID = onPlayingMatchViewDao.HomeTeamID
          AwayTeamID = onPlayingMatchViewDao.AwayTeamID
          MatchDate = onPlayingMatchViewDao.MatchDate
          HomeTeamName = onPlayingMatchViewDao.HomeTeamName
          AwayTeamName = onPlayingMatchViewDao.AwayTeamName
          GroupMatchID = onPlayingMatchViewDao.GroupMatchID }
