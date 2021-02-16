namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindMatchesByGroupMatchAction(repository: IQueryRepository<MatchGroupViewDao>) =

    interface IFindMatchesByGroupMatchAction with

        member this.AsyncExecute(groupMatchID: Uuid): Async<MatchGroupViewDao seq> =
            async {
                let! matches = repository.AsyncFind(MatchesByGroupMatchQueryObject(groupMatchID |> Uuid.toGuid))

                return
                    matches
                    |> Seq.map (fun x -> x :?> MatchGroupViewDao)
            }
