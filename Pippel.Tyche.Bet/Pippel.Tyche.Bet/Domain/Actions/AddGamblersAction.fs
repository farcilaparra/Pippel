namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddGamblersAction(gamblerRepository: IGamblerRepository,
                       unitOfWork: IUnitOfWork,
                       gamblerMapper: IMapper<Gambler, GamblerDao>) =
    inherit AddAction<GamblerDao, Gambler>(gamblerRepository, unitOfWork, gamblerMapper)
