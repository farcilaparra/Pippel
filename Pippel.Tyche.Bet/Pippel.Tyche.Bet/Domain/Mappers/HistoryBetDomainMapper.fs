namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type HistoryBetDomainMapper() =
    interface IMapper<HistoryBet, HistoryBetDao> with

        /// <summary>Maps from <c>HistoryBet</c> to <c>HistoryBetDao</c></summary>
        member this.MapToTarget(historyBet: HistoryBet): HistoryBetDao =
            { HistoryBetDao.ID = historyBet.ID |> Uuid.toGuid
              BetID = historyBet.BetID |> Uuid.toGuid
              HomeTeamValue = historyBet.HomeTeamValue |> PositiveInt.value
              AwayTeamValue = historyBet.AwayTeamValue |> PositiveInt.value
              CreationDate = historyBet.CreationDate }


        /// <summary>Maps from <c>HistoryBetDao</c> to <c>HistoryBet</c></summary>
        member this.MapToSource(historyBetDao: HistoryBetDao): HistoryBet =
            { HistoryBet.ID = historyBetDao.ID |> Uuid.createFromGuid
              BetID = historyBetDao.BetID |> Uuid.createFromGuid
              HomeTeamValue = historyBetDao.HomeTeamValue |> PositiveInt.create
              AwayTeamValue = historyBetDao.AwayTeamValue |> PositiveInt.create
              CreationDate = historyBetDao.CreationDate }
