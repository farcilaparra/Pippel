namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type GroupMatchRepositoryInDB(context: Context) =
    inherit Repository<GroupMatchDao>(context)
    interface IGroupMatchRepository
