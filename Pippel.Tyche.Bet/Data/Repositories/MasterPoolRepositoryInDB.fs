namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type MasterPoolRepositoryInDB(context: Context) =
    inherit Repository<MasterPoolDao>(context)
    interface IMasterPoolRepository
