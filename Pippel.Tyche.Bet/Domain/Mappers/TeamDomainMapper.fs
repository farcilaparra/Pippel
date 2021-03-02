namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module TeamDomainMapper =

    let mapFromDomain (team: TeamDomain) : TeamDao =
        { TeamDao.TeamID = team.ID.TeamID
          Name = team.Name }

    let mapToDomain (teamDao: TeamDao) : TeamDomain =
        { ID = { TeamID = teamDao.TeamID }
          Name = teamDao.Name }
