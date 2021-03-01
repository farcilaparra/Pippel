namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type RoundRepositoryInDB(context: Context) =
    inherit Repository<RoundDao>(context)
    interface IRoundRepository
