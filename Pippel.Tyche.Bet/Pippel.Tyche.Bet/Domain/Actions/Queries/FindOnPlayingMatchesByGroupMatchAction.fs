namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindOnPlayingMatchesByGroupMatchAction(repository: IQueryRepository<OnPlayingMatchViewDao>,
                                            findGroupBetByKeyAction: IFindGroupBetByKeyAction) =

    interface IFindOnPlayingMatchesByGroupMatchAction with

        member this.AsyncExecute(groupBetID: Uuid): Async<OnPlayingMatchViewDao seq> =
            async {
                let! groupBet = findGroupBetByKeyAction.AsyncExecute [| groupBetID |]

                let! matches =
                    repository.AsyncFind(OnPlayingMatchesByGroupMatchQueryObject(groupBet.GroupMatchID |> Uuid.toGuid))

                return
                    matches
                    |> Seq.map (fun x -> x :?> OnPlayingMatchViewDao)
            }
