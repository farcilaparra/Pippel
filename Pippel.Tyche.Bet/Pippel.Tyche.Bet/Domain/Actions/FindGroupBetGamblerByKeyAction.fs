namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindGroupBetGamblerByKeyAction(groupBetGamblerRepository: IGroupBetGamblerRepository,
                                    groupBetGamblerMapper: IMapper<GroupBetGambler, GroupBetGamblerDao>) =
    inherit FindByKeyAction<GroupBetGamblerDao, GroupBetGambler>(groupBetGamblerRepository, groupBetGamblerMapper)

    interface IFindGroupBetGamblerByKeyAction with
    
        override this.AsyncExecute(key: obj []): Async<GroupBetGambler> =
            base.AsyncExecute
                (key
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
