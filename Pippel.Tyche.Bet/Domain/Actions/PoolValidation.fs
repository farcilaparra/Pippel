namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type
open Pippel.Type.Validation

module PoolValidation =

    let (|NotExist|_|) (repository: IPoolRepository) (key: PoolPK) =
        try
            let _ =
                async { return! repository.AsyncFindByKey [| key.PoolID |> Uuid.value |] }
                |> Async.RunSynchronously

            false
        with
        | :? NotFoundException -> true
        | _ -> reraise ()
        |> ifTrueThen NotExist

    let (|MasterPoolDoesNotExist|_|) (masterPoolRepository: IMasterPoolRepository) (poolDomain: PoolDomain) =
        match { MasterPoolPK.MasterPoolID = poolDomain.MasterPoolID } with
        | MasterPoolValidation.NotExist masterPoolRepository -> true
        | _ -> false
        |> ifTrueThen MasterPoolDoesNotExist

    let (|GamblerDoesNotExist|_|) (gamblerRepository: IGamblerRepository) (poolDomain: PoolDomain) =
        match { GamblerPK.UserID = poolDomain.OwnerGamblerID } with
        | GamblerValidation.NotExist gamblerRepository -> true
        | _ -> false
        |> ifTrueThen GamblerDoesNotExist
