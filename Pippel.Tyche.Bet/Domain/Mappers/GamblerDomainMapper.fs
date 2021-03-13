namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module GamblerDomainMapper =

    let mapFromDomain (gamblerDomain: GamblerDomain) : GamblerDao =
        { GamblerDao.UserID = gamblerDomain.ID.UserID |> Uuid.value }

    let mapToDomain (gamblerDao: GamblerDao) : GamblerDomain =
        { GamblerDomain.ID = { UserID = gamblerDao.UserID |> Uuid.from } }
