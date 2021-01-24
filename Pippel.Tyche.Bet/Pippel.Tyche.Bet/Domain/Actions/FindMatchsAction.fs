namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindMatchsAction(matchRepository: IMatchRepository, matchMapper: IMapper<Match, MatchDao>) =
    inherit FindAction<MatchDao, Match>(matchRepository, matchMapper)
