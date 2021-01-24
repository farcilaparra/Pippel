namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type UpdateGroupBetGamblersAction(groupBetGamblerRepository: IGroupBetGamblerRepository,
                                  unitOfWork: IUnitOfWork,
                                  groupBetGamblerMapper: IMapper<GroupBetGambler, GroupBetGamblerDao>) =
    inherit UpdateAction<GroupBetGamblerDao, GroupBetGambler>(groupBetGamblerRepository,
                                                              unitOfWork,
                                                              groupBetGamblerMapper)
