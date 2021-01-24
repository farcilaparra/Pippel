namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type GamblerDomainMapper() =
    interface IMapper<Gambler, GamblerDao> with

        /// <summary>Maps from <c>Gambler</c> to <c>GamblerDao</c></summary>
        member this.MapToTarget(gambler: Gambler): GamblerDao =
            { GamblerDao.ID = gambler.ID |> Uuid.toGuid}


        /// <summary>Maps from <c>GamblerDao</c> to <c>Gambler</c></summary>
        member this.MapToSource(gamblerDao: GamblerDao): Gambler =
            { Gambler.ID = gamblerDao.ID |> Uuid.createFromGuid}
