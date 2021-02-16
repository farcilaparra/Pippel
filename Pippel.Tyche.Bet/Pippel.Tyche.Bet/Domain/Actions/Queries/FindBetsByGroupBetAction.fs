namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindBetsByGroupBetAction(repository: IQueryRepository<BetPositionViewDao>) =

    interface IFindBetsByGroupBetAction with

        member this.AsyncExecute(groupBetID: Uuid): Async<BetPositionViewDao seq> =
            async {
                let! bets = repository.AsyncFind(BetsByGroupBetQueryObject(groupBetID |> Uuid.toGuid))

                return
                    bets
                    |> Seq.map (fun x -> x :?> BetPositionViewDao)
            }
