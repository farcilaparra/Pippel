namespace Pippel.Tyche.Bet.Domain.Actions.Queries

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

[<Interface>]
type IFindOpenedMasterPoolsByGamblerAction =

    abstract AsyncExecute : Uuid -> NotEmptyString option -> PositiveInt -> PositiveInt -> Async<PoolReviewViewDao Page>
