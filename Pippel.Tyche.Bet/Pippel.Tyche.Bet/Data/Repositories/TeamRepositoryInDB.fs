namespace Pippel.Tyche.Bet.Data.Repositories

open Pippel.Data.EntityFrameworkCore
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models

type TeamRepositoryInDB(context: Context) =
    inherit Repository<TeamDao>(context)
    interface ITeamRepository
