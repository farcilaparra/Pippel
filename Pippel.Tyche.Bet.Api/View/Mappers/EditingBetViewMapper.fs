namespace Pippel.Tyche.Bet.Api.Domain.Mappers

open Pippel.Tyche.Bet.Api.Data.Models
open Pippel.Tyche.Bet.Domain.Models

module EditingBetViewMapper =

    let mapFromView (editingBetDto: EditingBetDto) : BetDomain =
        { BetDomain.ID =
              { PoolID = editingBetDto.PoolID
                GamblerID = editingBetDto.GamblerID
                MatchID = editingBetDto.MatchID }
          HomeTeamValue = editingBetDto.HomeTeamValue
          AwayTeamValue = editingBetDto.AwayTeamValue }

    let mapToView (betDomain: BetDomain) : EditingBetDto =
        { EditingBetDto.PoolID = betDomain.ID.PoolID
          GamblerID = betDomain.ID.GamblerID
          MatchID = betDomain.ID.MatchID
          HomeTeamValue = betDomain.HomeTeamValue
          AwayTeamValue = betDomain.AwayTeamValue }
