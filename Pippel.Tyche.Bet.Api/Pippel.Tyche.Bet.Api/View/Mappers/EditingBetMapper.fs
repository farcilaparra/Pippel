namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

type EditingBetMapper() =

        /// <summary>Maps from <c>EditingBetDto</c> to <c>EditingBet</c></summary>
        member this.MapToEditingBet(editingBetDto: EditingBetDto): EditingBet =
            { EditingBet.GroupBetID = editingBetDto.GroupBetID |> Uuid.createFromGuid
              GamblerID = editingBetDto.GamblerID |> Uuid.createFromGuid
              MatchID = editingBetDto.MatchID |> Uuid.createFromGuid
              HomeValue = editingBetDto.HomeTeamValue |> PositiveInt.create
              AwayValue = editingBetDto.AwayTeamValue |> PositiveInt.create }
