namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddGroupBetGamblersAction(groupBetGamblerRepository: IGroupBetGamblerRepository,
                               unitOfWork: IUnitOfWork,
                               groupBetGamblerMapper: IMapper<GroupBetGambler, GroupBetGamblerDao>) =
    inherit AddAction<GroupBetGamblerDao, GroupBetGambler>(groupBetGamblerRepository, unitOfWork, groupBetGamblerMapper)
