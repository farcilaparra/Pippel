namespace Pippel.Tyche.Bet.Actions

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type
open Pippel.Tyche.Bet.Actions.Queries

type FindOpenedGroupsMatchesAction(repository: IQueryRepository<MatchGamblerViewDao>) =

    member this.AsyncExecute(gamblerID: Uuid): Async<MatchGamblerViewDao seq> =
        async {
            let! items = repository.AsyncFind(OpenedMatchGamblerByGamblerQueryObject(gamblerID |> Uuid.toGuid))

            return
                items
                |> Seq.map (fun x -> x :?> MatchGamblerViewDao)
        }
