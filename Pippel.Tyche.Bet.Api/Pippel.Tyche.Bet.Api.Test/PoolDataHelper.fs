module PoolDataHelper

open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

let createPoolsToReturn () =
    [| { PoolDomain.ID = { PoolID = Uuid.From "718d7467-383f-4094-81c4-5b104d7969aa" }
         MasterPoolID = Uuid.newUuid ()
         OwnerGamblerID = Uuid.newUuid ()
         CreationDate = DateTime.now ()
         Name = NotEmptyString100.From "Copa América de Pippel 2021" } |]
