namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindTeamsAction(teamRepository: ITeamRepository, teamMapper: IMapper<Team, TeamDao>) =
    inherit FindAction<TeamDao, Team>(teamRepository, teamMapper)
