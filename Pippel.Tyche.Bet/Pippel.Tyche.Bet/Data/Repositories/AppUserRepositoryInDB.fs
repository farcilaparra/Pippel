namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models
    
type AppUserRepositoryInDB(context: Context) =
     inherit Repository<AppUserDao>(context)
     interface IAppUserRepository
