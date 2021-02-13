namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindGamblersAction(gamblerRepository: IGamblerRepository, gamblerMapper: IMapper<Gambler, GamblerDao>) =
    inherit FindAction<GamblerDao, Gambler>(gamblerRepository, gamblerMapper)
