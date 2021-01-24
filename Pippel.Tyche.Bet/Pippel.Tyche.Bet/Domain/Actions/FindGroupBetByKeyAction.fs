namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindGroupBetByKeyAction(groupBetRepository: IGroupBetRepository, groupBetMapper: IMapper<GroupBet, GroupBetDao>) =
    inherit FindByKeyAction<GroupBetDao, GroupBet>(groupBetRepository, groupBetMapper)

    override this.AsyncExecute(key: obj[]): Async<GroupBet> =
        base.AsyncExecute(key |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
    