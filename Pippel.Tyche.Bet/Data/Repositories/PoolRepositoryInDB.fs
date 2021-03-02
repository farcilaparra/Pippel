namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type PoolRepositoryInDB(context: Context) =
    inherit Repository<PoolDao>(context)
    interface IPoolRepository
