namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateGamblersAction(gamblerRepository: IGamblerRepository,
                          unitOfWork: IUnitOfWork,
                          gamblerMapper: IMapper<Gambler, GamblerDao>) =
    inherit UpdateAction<GamblerDao, Gambler>(gamblerRepository, unitOfWork, gamblerMapper)