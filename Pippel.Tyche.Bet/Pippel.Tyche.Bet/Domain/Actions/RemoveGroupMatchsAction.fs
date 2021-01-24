namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveGroupMatchsAction(groupMatchRepository: IGroupMatchRepository,
                             unitOfWork: IUnitOfWork,
                             groupMatchMapper: IMapper<GroupMatch, GroupMatchDao>) =
    inherit RemoveAction<GroupMatchDao, GroupMatch>(groupMatchRepository, unitOfWork, groupMatchMapper)

    override this.AsyncExecute(key: obj [] seq): Async<GroupMatch seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
