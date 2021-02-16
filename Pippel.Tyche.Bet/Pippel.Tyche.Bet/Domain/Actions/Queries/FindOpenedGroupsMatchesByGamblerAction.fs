namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindOpenedGroupsMatchesByGamblerAction(repository: IQueryRepository<MatchGroupGamblerViewDao>) =

    interface IFindOpenedGroupsMatchesByGamblerAction with
    
        member this.AsyncExecute(gamblerID: Uuid): Async<MatchGroupGamblerViewDao seq> =
            async {
                let! items = repository.AsyncFind(OpenedGroupsMatchesByGamblerQueryObject(gamblerID |> Uuid.toGuid))

                return
                    items
                    |> Seq.map (fun x -> x :?> MatchGroupGamblerViewDao)
            }
