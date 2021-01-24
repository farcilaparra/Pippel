namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindGroupBetsAction(groupBetRepository: IGroupBetRepository, groupBetMapper: IMapper<GroupBet, GroupBetDao>) =
    inherit FindAction<GroupBetDao, GroupBet>(groupBetRepository, groupBetMapper)
