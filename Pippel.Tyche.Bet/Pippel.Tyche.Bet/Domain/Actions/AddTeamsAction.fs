namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddTeamsAction(teamRepository: ITeamRepository, unitOfWork: IUnitOfWork, teamMapper: IMapper<Team, TeamDao>) =
    inherit AddAction<TeamDao, Team>(teamRepository, unitOfWork, teamMapper)
