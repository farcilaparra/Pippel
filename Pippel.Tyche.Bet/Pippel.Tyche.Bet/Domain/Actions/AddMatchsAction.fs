namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddMatchsAction(matchRepository: IMatchRepository, unitOfWork: IUnitOfWork, matchMapper: IMapper<Match, MatchDao>) =
    inherit AddAction<MatchDao, Match>(matchRepository, unitOfWork, matchMapper)