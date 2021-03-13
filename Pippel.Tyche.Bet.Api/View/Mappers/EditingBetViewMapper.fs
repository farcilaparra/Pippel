namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

module EditingBetViewMapper =

    let mapFromView (editingBetDto: EditingBetDto) : BetDomain =
        { BetDomain.ID =
              { PoolID = editingBetDto.PoolID |> Uuid.from
                GamblerID = editingBetDto.GamblerID |> Uuid.from
                MatchID = editingBetDto.MatchID |> Uuid.from }
          HomeTeamValue = editingBetDto.HomeTeamValue |> PositiveInt.from
          AwayTeamValue = editingBetDto.AwayTeamValue |> PositiveInt.from }

    let mapToView (betDomain: BetDomain) : EditingBetDto =
        { EditingBetDto.PoolID = betDomain.ID.PoolID |> Uuid.value
          GamblerID = betDomain.ID.GamblerID |> Uuid.value
          MatchID = betDomain.ID.MatchID |> Uuid.value
          HomeTeamValue = betDomain.HomeTeamValue |> PositiveInt.value
          AwayTeamValue = betDomain.AwayTeamValue |> PositiveInt.value }
