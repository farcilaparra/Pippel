namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type TeamDomainMapper() =
    interface IMapper<Team, TeamDao> with

        /// <summary>Maps from <c>Team</c> to <c>TeamDao</c></summary>
        member this.MapToTarget(team: Team): TeamDao =
            { TeamDao.ID = team.ID |> Uuid.toGuid
              TeamName = team.TeamName |> NonEmptyString.value }

        /// <summary>Maps from <c>TeamDao</c> to <c>Team</c></summary>
        member this.MapToSource(teamDao: TeamDao): Team =
            { Team.ID = teamDao.ID |> Uuid.createFromGuid
              TeamName = teamDao.TeamName |> NonEmptyString.create }
