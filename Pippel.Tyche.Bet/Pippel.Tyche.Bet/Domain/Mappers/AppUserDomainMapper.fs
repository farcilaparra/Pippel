namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type AppUserDomainMapper() =
    interface IMapper<AppUser, AppUserDao> with

        /// <summary>Maps from <c>AppUser</c> to <c>AppUserDao</c></summary>
        member this.MapToTarget(appUser: AppUser): AppUserDao =
            { AppUserDao.ID = appUser.ID |> Uuid.toGuid
              Email = appUser.Email |> NonEmptyString.value
              Password = appUser.Password |> NonEmptyString.value }


        /// <summary>Maps from <c>AppUserDao</c> to <c>AppUser</c></summary>
        member this.MapToSource(appUserDao: AppUserDao): AppUser =
            { AppUser.ID = appUserDao.ID |> Uuid.createFromGuid
              Email = appUserDao.Email |> NonEmptyString.create
              Password = appUserDao.Password |> NonEmptyString.create }
