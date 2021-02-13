namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindHistoryBetsAction(historyBetRepository: IHistoryBetRepository,
                           historyBetMapper: IMapper<HistoryBet, HistoryBetDao>) =
    inherit FindAction<HistoryBetDao, HistoryBet>(historyBetRepository, historyBetMapper)
