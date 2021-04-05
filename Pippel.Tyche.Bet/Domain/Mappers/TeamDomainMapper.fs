namespace Pippel.Tyche.Bet.Domain.Mappers

open System
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module TeamDomainMapper =

    let mapFromDomain (team: TeamDomain): TeamDao =
        { TeamDao.TeamID = team.ID.TeamID |> Uuid.value
          Name = team.Name |> String.value }

    let mapToDomain (teamDao: TeamDao): TeamDomain =
        try
            { ID = { TeamID = Uuid.From teamDao.TeamID }
              Name = NotEmptyString100.From teamDao.Name }
        with :? ArgumentException as ex -> raise <| DomainValueException(ex.Message)
