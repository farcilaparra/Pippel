namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type BetConfigRepositoryInDB(context: Context) =
    inherit Repository<BetConfigDao>(context)
    interface IBetConfigRepository
