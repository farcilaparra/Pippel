namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindBetConfigsAction(betConfigRepository: IBetConfigRepository, betConfigMapper: IMapper<BetConfig, BetConfigDao>) =
    inherit FindAction<BetConfigDao, BetConfig>(betConfigRepository, betConfigMapper)
