namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type GroupBetGamblerRepositoryInDB(context: Context) =
    inherit Repository<GroupBetGamblerDao>(context)
    interface IGroupBetGamblerRepository
