namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateTeamsAction(teamRepository: ITeamRepository, unitOfWork: IUnitOfWork, teamMapper: IMapper<Team, TeamDao>) =
    inherit UpdateAction<TeamDao, Team>(teamRepository, unitOfWork, teamMapper)
