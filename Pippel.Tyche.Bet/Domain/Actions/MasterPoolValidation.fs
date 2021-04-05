namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type
open Pippel.Type.Validation

module MasterPoolValidation =

    let (|NotExist|_|) (repository: IMasterPoolRepository) (key: MasterPoolPK) =
        try
            let _ =
                async { return! repository.AsyncFindByKey [| key.MasterPoolID |> Uuid.value |] }
                |> Async.RunSynchronously

            false
        with
        | :? NotFoundException -> true
        | _ -> reraise ()
        |> ifTrueThen NotExist
