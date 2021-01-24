namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateBetsAction(betRepository: IBetRepository, unitOfWork: IUnitOfWork, betMapper: IMapper<Bet, BetDao>) =
    inherit UpdateAction<BetDao, Bet>(betRepository, unitOfWork, betMapper)
