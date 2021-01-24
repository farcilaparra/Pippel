namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type RoundMatchRepositoryInDB(context: Context) =
    inherit Repository<RoundMatchDao>(context)
    interface IRoundMatchRepository
