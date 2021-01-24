namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddBetConfigsAction(betConfigRepository: IBetConfigRepository,
                         unitOfWork: IUnitOfWork,
                         betConfigMapper: IMapper<BetConfig, BetConfigDao>) =
    inherit AddAction<BetConfigDao, BetConfig>(betConfigRepository, unitOfWork, betConfigMapper)
