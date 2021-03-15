namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Type
open Pippel.Type

module EditingBetViewMapper =

    let mapFromView (editingBetDto: EditingBetDto) : BetDomain =
        { BetDomain.ID =
              { PoolID = Uuid.From editingBetDto.PoolID
                GamblerID = Uuid.From editingBetDto.GamblerID
                MatchID = Uuid.From editingBetDto.MatchID }
          HomeTeamValue = Score.From editingBetDto.HomeTeamValue
          AwayTeamValue = Score.From editingBetDto.AwayTeamValue }

    let mapToView (betDomain: BetDomain) : EditingBetDto =
        { EditingBetDto.PoolID = betDomain.ID.PoolID |> Uuid.value
          GamblerID = betDomain.ID.GamblerID |> Uuid.value
          MatchID = betDomain.ID.MatchID |> Uuid.value
          HomeTeamValue = betDomain.HomeTeamValue |> Number.value
          AwayTeamValue = betDomain.AwayTeamValue |> Number.value }
