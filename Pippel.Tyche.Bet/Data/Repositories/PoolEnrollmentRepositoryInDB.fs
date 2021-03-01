namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type PoolEnrollmentRepositoryInDB(context: Context) =
    inherit Repository<PoolEnrollmentDao>(context)
    interface IPoolEnrollmentRepository
