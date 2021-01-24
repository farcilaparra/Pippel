namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveAppUsersAction(appUserRepository: IAppUserRepository,
                          unitOfWork: IUnitOfWork,
                          appUserMapper: IMapper<AppUser, AppUserDao>) =
    inherit RemoveAction<AppUserDao, AppUser>(appUserRepository, unitOfWork, appUserMapper)

    override this.AsyncExecute(key: obj [] seq): Async<AppUser seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
