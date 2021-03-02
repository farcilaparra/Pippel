namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type MatchRepositoryInDB(context: Context) =
    inherit Repository<MatchDao>(context)
    interface IMatchRepository
