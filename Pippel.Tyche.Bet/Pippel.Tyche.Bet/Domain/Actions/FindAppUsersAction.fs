namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models

type FindAppUsersAction(appUserRepository: IAppUserRepository, appUserMapper: IMapper<AppUser, AppUserDao>) =
    inherit FindAction<AppUserDao, AppUser>(appUserRepository, appUserMapper)
