namespace Pippel.Tyche.Admin.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindGroupMatchByKeyAction(groupMatchRepository: IGroupMatchRepository,
                               groupMatchMapper: IMapper<GroupMatch, GroupMatchDao>) =
    inherit FindByKeyAction<GroupMatchDao, GroupMatch>(groupMatchRepository, groupMatchMapper)

    override this.AsyncExecute(key: obj []): Async<GroupMatch> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
