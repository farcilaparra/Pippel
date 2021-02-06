namespace Pippel.Tyche.Bet.Data.Repositories.Queries

open Pippel.Data
open Pippel.Tyche.Bet

type QueryRepositoryFactory(context: QueryContext) =

    interface IQueryRepositoryFactory with

        member this.Get<'T when 'T: not struct>() =
            QueryRepositoryInDB<'T>(context) :> IQueryRepository<'T>
