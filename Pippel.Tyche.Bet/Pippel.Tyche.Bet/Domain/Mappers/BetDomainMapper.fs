namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type BetDomainMapper() =
    interface IMapper<Bet, BetDao> with

        /// <summary>Maps from <c>Bet</c> to <c>BetDao</c></summary>
        member this.MapToTarget(bet: Bet): BetDao =
            { BetDao.ID = bet.ID |> Uuid.toGuid
              GroupBetID = bet.GroupBetID |> Uuid.toGuid
              GamblerID = bet.GamblerID |> Uuid.toGuid
              MatchID = bet.MatchID |> Uuid.toGuid
              HomeTeamValue = bet.HomeTeamValue |> PositiveInt.value
              AwayTeamValue = bet.AwayTeamValue |> PositiveInt.value }


        /// <summary>Maps from <c>BetDao</c> to <c>Bet</c></summary>
        member this.MapToSource(betDao: BetDao): Bet =
            { Bet.ID = betDao.ID |> Uuid.createFromGuid
              GroupBetID = betDao.GroupBetID |> Uuid.createFromGuid
              GamblerID = betDao.GamblerID |> Uuid.createFromGuid
              MatchID = betDao.MatchID |> Uuid.createFromGuid
              HomeTeamValue = betDao.HomeTeamValue |> PositiveInt.create
              AwayTeamValue = betDao.AwayTeamValue |> PositiveInt.create }
