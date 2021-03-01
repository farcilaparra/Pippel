namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models

module GamblerDomainMapper =

    let mapFromDomain (gamblerDomain: GamblerDomain) : GamblerDao =
        { GamblerDao.UserID = gamblerDomain.ID.UserID }

    let mapToDomain (gamblerDao: GamblerDao) : GamblerDomain =
        { GamblerDomain.ID = { UserID = gamblerDao.UserID } }
