namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindGroupBetByKeyAction(groupBetRepository: IGroupBetRepository, groupBetMapper: IMapper<GroupBet, GroupBetDao>) =
    inherit FindByKeyAction<GroupBetDao, GroupBet>(groupBetRepository, groupBetMapper)

    interface IFindGroupBetByKeyAction with

        member this.AsyncExecute(key: obj []): Async<GroupBet> =
            base.AsyncExecute
                (key
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
