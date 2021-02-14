namespace Pippel.Tyche.Bet.Domain.Mappers

open System
open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type MatchDomainMapper() =
    interface IMapper<Match, MatchDao> with

        /// <summary>Maps from <c>Match</c> to <c>MatchDao</c></summary>
        member this.Map(matchOrGame: Match): MatchDao =
            { MatchDao.ID = matchOrGame.ID |> Uuid.toGuid
              HomeTeamID = matchOrGame.HomeTeamID |> Uuid.toGuid
              AwayTeamID = matchOrGame.AwayTeamID |> Uuid.toGuid
              RoundMatchID = matchOrGame.RoundMatchID |> Uuid.toGuid
              MatchDate = matchOrGame.MatchDate
              HomeResult =
                  match matchOrGame.HomeResult with
                  | Some value -> Nullable<int>(value |> PositiveInt.value)
                  | None -> Nullable<int>()
              AwayResult =
                  match matchOrGame.AwayResult with
                  | Some value -> Nullable<int>(value |> PositiveInt.value)
                  | None -> Nullable<int>()
              Status = matchOrGame.Status }

        /// <summary>Maps from <c>MatchDao</c> to <c>Match</c></summary>
        member this.Map(matchDao: MatchDao): Match =
            { Match.ID = matchDao.ID |> Uuid.createFromGuid
              HomeTeamID = matchDao.HomeTeamID |> Uuid.createFromGuid
              AwayTeamID = matchDao.AwayTeamID |> Uuid.createFromGuid
              RoundMatchID = matchDao.RoundMatchID |> Uuid.createFromGuid
              MatchDate = matchDao.MatchDate
              HomeResult =
                  match matchDao.HomeResult.HasValue with
                  | true -> Some(matchDao.HomeResult.Value |> PositiveInt.create)
                  | false -> None
              AwayResult =
                  match matchDao.AwayResult.HasValue with
                  | true -> Some(matchDao.AwayResult.Value |> PositiveInt.create)
                  | false -> None
              Status = matchDao.Status }
