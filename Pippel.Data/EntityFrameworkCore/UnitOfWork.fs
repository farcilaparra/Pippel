namespace Pippel.Data.EntityFrameworkCore

open Microsoft.EntityFrameworkCore
open Pippel.Data

type UnitOfWork(context: DbContext) =
    interface IUnitOfWork with

        member this.SaveChanges() = context.SaveChanges() |> ignore
