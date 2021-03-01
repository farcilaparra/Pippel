namespace Pippel.Tyche.Bet.Data.Repositories.Queries

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet

type QueryRepositoryInDB<'T when 'T: not struct>(context: QueryContext) =
    inherit QueryRepository<'T>(context)
