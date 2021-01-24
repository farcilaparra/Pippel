namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateGroupBetsAction(groupBetRepository: IGroupBetRepository,
                           unitOfWork: IUnitOfWork,
                           groupBetMapper: IMapper<GroupBet, GroupBetDao>) =
    inherit UpdateAction<GroupBetDao, GroupBet>(groupBetRepository, unitOfWork, groupBetMapper)
