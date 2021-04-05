namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Data.Actions
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open PoolValidation

type AddPoolsAction(poolRepository: IPoolRepository,
                    masterPoolRepository: IMasterPoolRepository,
                    gamblerRepository: IGamblerRepository) =

    let validate pools =
        pools
        |> Seq.iter (fun it ->
            match it with
            | MasterPoolDoesNotExist masterPoolRepository ->
                raise
                <| DomainException($"Master Pool of {it} doesn't exist")
            | GamblerDoesNotExist gamblerRepository ->
                raise
                <| DomainException($"Gambler of {it} doesn't exist")
            | _ -> ())

    interface IAddPoolsAction with

        member this.AsyncExecute(pools: PoolDomain seq) =
            async {
                validate pools

                return!
                    poolRepository
                    |> asyncAdd pools PoolDomainMapper.mapFromDomain PoolDomainMapper.mapToDomain
            }
