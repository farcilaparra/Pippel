namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateMatchsAction(matchRepository: IMatchRepository,
                        unitOfWork: IUnitOfWork,
                        matchMapper: IMapper<Match, MatchDao>) =
    inherit UpdateAction<MatchDao, Match>(matchRepository, unitOfWork, matchMapper)
