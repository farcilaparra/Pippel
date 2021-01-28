namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

type BetViewMapper() =
    interface IMapper<BetDto, Bet> with

        /// <summary>Maps from <c>BetDto</c> to <c>Bet</c></summary>
        member this.MapToTarget(betDto: BetDto): Bet =
            { Bet.ID = betDto.ID |> Uuid.createFromGuid
              GroupBetID = betDto.GroupBetID |> Uuid.createFromGuid
              GamblerID = betDto.GamblerID |> Uuid.createFromGuid
              MatchID = betDto.MatchID |> Uuid.createFromGuid
              HomeTeamValue = betDto.HomeTeamValue |> PositiveInt32.create
              AwayTeamValue = betDto.AwayTeamValue |> PositiveInt32.create
              LastPosition = betDto.LastPosition |> PositiveInt32.create }


        /// <summary>Maps from <c>Bet</c> to <c>BetDto</c></summary>
        member this.MapToSource(bet: Bet): BetDto =
            { BetDto.ID = bet.ID |> Uuid.toGuid
              GroupBetID = bet.GroupBetID |> Uuid.toGuid
              GamblerID = bet.GamblerID |> Uuid.toGuid
              MatchID = bet.MatchID |> Uuid.toGuid
              HomeTeamValue = bet.HomeTeamValue |> PositiveInt32.value
              AwayTeamValue = bet.AwayTeamValue |> PositiveInt32.value
              LastPosition = bet.LastPosition |> PositiveInt32.value }
