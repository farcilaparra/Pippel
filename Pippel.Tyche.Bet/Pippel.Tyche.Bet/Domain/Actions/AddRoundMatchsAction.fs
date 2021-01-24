namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddRoundMatchsAction(roundMatchRepository: IRoundMatchRepository,
                          unitOfWork: IUnitOfWork,
                          roundMatchMapper: IMapper<RoundMatch, RoundMatchDao>) =
    inherit AddAction<RoundMatchDao, RoundMatch>(roundMatchRepository, unitOfWork, roundMatchMapper)
