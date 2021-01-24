namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateHistoryBetsAction(historyBetRepository: IHistoryBetRepository,
                             unitOfWork: IUnitOfWork,
                             historyBetMapper: IMapper<HistoryBet, HistoryBetDao>) =
    inherit UpdateAction<HistoryBetDao, HistoryBet>(historyBetRepository, unitOfWork, historyBetMapper)
