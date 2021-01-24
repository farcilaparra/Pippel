namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindRoundMatchsAction(roundMatchRepository: IRoundMatchRepository,
                           roundMatchMapper: IMapper<RoundMatch, RoundMatchDao>) =
    inherit FindAction<RoundMatchDao, RoundMatch>(roundMatchRepository, roundMatchMapper)
