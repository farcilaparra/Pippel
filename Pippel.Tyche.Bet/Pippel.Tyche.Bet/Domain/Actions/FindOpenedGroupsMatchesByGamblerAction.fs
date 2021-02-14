namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Actions.Queries

type FindOpenedGroupsMatchesByGamblerAction(repository: IQueryRepository<MatchGroupGamblerViewDao>) =

    interface IFindOpenedGroupsMatchesByGamblerAction with
    
        member this.AsyncExecute(gamblerID: Uuid): Async<MatchGroupGamblerViewDao seq> =
            async {
                let! items = repository.AsyncFind(OpenedGroupsMatchesByGamblerQueryObject(gamblerID |> Uuid.toGuid))

                return
                    items
                    |> Seq.map (fun x -> x :?> MatchGroupGamblerViewDao)
            }
