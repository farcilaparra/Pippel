namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindBetsAction(betRepository: IBetRepository, betMapper: IMapper<Bet, BetDao>) =
    inherit FindAction<BetDao, Bet>(betRepository, betMapper)
