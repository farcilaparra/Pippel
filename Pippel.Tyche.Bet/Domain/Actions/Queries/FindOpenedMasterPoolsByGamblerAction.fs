namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Core
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects
open Pippel.Type

type FindOpenedMasterPoolsByGamblerAction(repository: IQueryRepository<PoolReviewViewDao>) =

    interface IFindOpenedMasterPoolsByGamblerAction with

        member this.AsyncExecute
            (gamblerID: Uuid)
            (filter: NotEmptyString option)
            (skip: PositiveInt)
            (take: PositiveInt)
            : Async<PoolReviewViewDao Page> =
            async {
                return!
                    repository.AsyncFindWithPagination<PoolReviewViewDao>
                        (OpenedMasterPoolsByGamblerQueryObject(gamblerID, filter))
                        (skip |> PositiveInt.value)
                        (take |> PositiveInt.value)
            }
