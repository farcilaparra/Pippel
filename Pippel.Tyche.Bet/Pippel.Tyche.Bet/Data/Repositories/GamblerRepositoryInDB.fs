namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models
    
type GamblerRepositoryInDB(context: Context) =
     inherit Repository<GamblerDao>(context)
     interface IGamblerRepository
