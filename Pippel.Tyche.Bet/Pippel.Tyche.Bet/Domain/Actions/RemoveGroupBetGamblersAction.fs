namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveGroupBetGamblersAction(groupBetGamblerRepository: IGroupBetGamblerRepository,
                                  unitOfWork: IUnitOfWork,
                                  groupBetGamblerMapper: IMapper<GroupBetGambler, GroupBetGamblerDao>) =
    inherit RemoveAction<GroupBetGamblerDao, GroupBetGambler>(groupBetGamblerRepository,
                                                              unitOfWork,
                                                              groupBetGamblerMapper)

    override this.AsyncExecute(key: obj [] seq): Async<GroupBetGambler seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
