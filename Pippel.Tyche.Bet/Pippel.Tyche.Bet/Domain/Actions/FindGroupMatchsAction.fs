namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindgroupMatchsAction(groupMatchRepository: IGroupMatchRepository,
                           groupMatchMapper: IMapper<GroupMatch, GroupMatchDao>) =
    inherit FindAction<GroupMatchDao, GroupMatch>(groupMatchRepository, groupMatchMapper)
