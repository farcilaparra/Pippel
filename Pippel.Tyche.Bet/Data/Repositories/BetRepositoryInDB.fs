namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type BetRepositoryInDB(context: Context) =
    inherit Repository<BetDao>(context)
    interface IBetRepository
