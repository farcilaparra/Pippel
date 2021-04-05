namespace Pippel.Tyche.Bet.Domain.Mappers

open System
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

module GamblerDomainMapper =

    let mapFromDomain (gamblerDomain: GamblerDomain): GamblerDao =
        { GamblerDao.UserID = gamblerDomain.ID.UserID |> Uuid.value }

    let mapToDomain (gamblerDao: GamblerDao): GamblerDomain =
        try
            { GamblerDomain.ID = { UserID = Uuid.From gamblerDao.UserID } }
        with :? ArgumentException as ex -> raise <| DomainValueException(ex.Message)
