namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindAppUserByKeyAction(appUserRepository: IAppUserRepository, appUserMapper: IMapper<AppUser, AppUserDao>) =
    inherit FindByKeyAction<AppUserDao, AppUser>(appUserRepository, appUserMapper)

    override this.AsyncExecute(key: obj []): Async<AppUser> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
