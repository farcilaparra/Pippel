namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddAppUsersAction(appUserRepository: IAppUserRepository,
                       unitOfWork: IUnitOfWork,
                       appUserMapper: IMapper<AppUser, AppUserDao>) =
    inherit AddAction<AppUserDao, AppUser>(appUserRepository, unitOfWork, appUserMapper)