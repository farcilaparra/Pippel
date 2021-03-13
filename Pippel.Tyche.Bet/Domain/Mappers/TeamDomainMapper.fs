namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module TeamDomainMapper =

    let mapFromDomain (team: TeamDomain) : TeamDao =
        { TeamDao.TeamID = team.ID.TeamID |> Uuid.value
          Name = team.Name |> NotEmptyString.value }

    let mapToDomain (teamDao: TeamDao) : TeamDomain =
        { ID = { TeamID = teamDao.TeamID |> Uuid.from }
          Name = teamDao.Name |> NotEmptyString.from }
