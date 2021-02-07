namespace Pippel.Tyche.Bet.Actions

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type
open Pippel.Tyche.Bet.Actions.Queries

type FindMatchesByGroupBetAction(repository: IQueryRepository<MatchViewDao>) =

    member this.AsyncExecute(groupBetID: Uuid): Async<MatchViewDao seq> =
        async {
            let! items = repository.AsyncFind(MatchesByGroupBetQueryObject(groupBetID |> Uuid.toGuid))

            return
                items
                |> Seq.map (fun x -> x :?> MatchViewDao)
        }
