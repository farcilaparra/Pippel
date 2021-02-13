namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveHistoryBetsAction(historyBetRepository: IHistoryBetRepository,
                             unitOfWork: IUnitOfWork,
                             historyBetMapper: IMapper<HistoryBet, HistoryBetDao>) =
    inherit RemoveAction<HistoryBetDao, HistoryBet>(historyBetRepository, unitOfWork, historyBetMapper)

    override this.AsyncExecute(key: obj [] seq): Async<HistoryBet seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
