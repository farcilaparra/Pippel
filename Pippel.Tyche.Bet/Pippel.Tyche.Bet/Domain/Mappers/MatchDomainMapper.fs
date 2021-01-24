namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type MatchDomainMapper() =
    interface IMapper<Match, MatchDao> with

        /// <summary>Maps from <c>Match</c> to <c>MatchDao</c></summary>
        member this.MapToTarget(matchOrGame: Match): MatchDao =
            { MatchDao.ID = matchOrGame.ID |> Uuid.toGuid
              HomeTeamID = matchOrGame.HomeTeamID |> Uuid.toGuid
              AwayTeamID = matchOrGame.AwayTeamID |> Uuid.toGuid
              RoundMatchID = matchOrGame.RoundMatchID |> Uuid.toGuid
              MatchDate = matchOrGame.MatchDate
              HomeResult = matchOrGame.HomeResult |> PositiveInt32.value
              AwayResult = matchOrGame.AwayResult |> PositiveInt32.value
              State = matchOrGame.State |> NonEmptyString.value }

        /// <summary>Maps from <c>MatchDao</c> to <c>Match</c></summary>
        member this.MapToSource(matchDao: MatchDao): Match =
            { Match.ID = matchDao.ID |> Uuid.createFromGuid
              HomeTeamID = matchDao.HomeTeamID |> Uuid.createFromGuid
              AwayTeamID = matchDao.AwayTeamID |> Uuid.createFromGuid
              RoundMatchID = matchDao.RoundMatchID |> Uuid.createFromGuid
              MatchDate = matchDao.MatchDate
              HomeResult = matchDao.HomeResult |> PositiveInt32.create
              AwayResult = matchDao.AwayResult |> PositiveInt32.create
              State = matchDao.State |> NonEmptyString.create }
