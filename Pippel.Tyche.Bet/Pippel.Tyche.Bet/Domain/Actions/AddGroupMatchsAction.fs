namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddGroupMatchsAction(groupMatchRepository: IGroupMatchRepository,
                          unitOfWork: IUnitOfWork,
                          groupMatchMapper: IMapper<GroupMatch, GroupMatchDao>) =
    inherit AddAction<GroupMatchDao, GroupMatch>(groupMatchRepository, unitOfWork, groupMatchMapper)
