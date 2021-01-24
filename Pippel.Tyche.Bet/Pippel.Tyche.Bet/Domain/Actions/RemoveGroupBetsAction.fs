namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveGroupBetsAction(groupBetRepository: IGroupBetRepository,
                           unitOfWork: IUnitOfWork,
                           groupBetMapper: IMapper<GroupBet, GroupBetDao>) =
    inherit RemoveAction<GroupBetDao, GroupBet>(groupBetRepository, unitOfWork, groupBetMapper)

    override this.AsyncExecute(key: obj [] seq): Async<GroupBet seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
