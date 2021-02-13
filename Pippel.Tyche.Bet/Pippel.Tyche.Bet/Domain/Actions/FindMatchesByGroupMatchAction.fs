namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Data
open Pippel.Tyche.Bet.Actions.Queries
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type FindMatchesByGroupMatchAction(repository: IQueryRepository<MatchGroupViewDao>) =

    interface IFindMatchesByGroupMatchAction with
    
        member this.AsyncExecute(groupMatchID: Uuid): Async<MatchGroupViewDao seq> =
            async {
                let groupMatchID = groupMatchID |> Uuid.toGuid
                let! matches = repository.AsyncFind(MatchesByGroupMatchQueryObject(groupMatchID))
                
                return
                    matches
                    |> Seq.map (fun x -> x :?> MatchGroupViewDao)
            }
