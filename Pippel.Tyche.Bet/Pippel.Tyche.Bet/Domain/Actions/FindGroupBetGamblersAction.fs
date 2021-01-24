namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindGroupBetGamblersAction(groupBetGamblerRepository: IGroupBetGamblerRepository,
                                groupBetGamblerMapper: IMapper<GroupBetGambler, GroupBetGamblerDao>) =
    inherit FindAction<GroupBetGamblerDao, GroupBetGambler>(groupBetGamblerRepository, groupBetGamblerMapper)