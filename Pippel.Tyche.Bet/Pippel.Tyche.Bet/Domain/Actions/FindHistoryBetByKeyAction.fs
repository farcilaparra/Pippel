namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindHistoryBetByKeyAction(historyBetRepository: IHistoryBetRepository,
                               historyBetMapper: IMapper<HistoryBet, HistoryBetDao>) =
    inherit FindByKeyAction<HistoryBetDao, HistoryBet>(historyBetRepository, historyBetMapper)

    override this.AsyncExecute(key: obj []): Async<HistoryBet> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
