namespace Pippel.Bet.Tyche.Api.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Pippel.Data
open Pippel.Tyche.Bet.Api.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Actions
open Pippel.Tyche.Bet.Api.Data.Models

[<ApiController>]
[<Route("[controller]")>]
type PoolController(logger: ILogger<PoolController>, addPoolAction: IAddPoolsAction, unitOfWork: IUnitOfWork) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.AsyncEditBet(poolsDtos: PoolDto []) =
        async {
            let! items =
                addPoolAction.AsyncExecute
                    (poolsDtos
                     |> Array.map (fun x -> x |> PoolViewMapper.mapFromView))

            unitOfWork.SaveChanges()

            return
                items
                |> Seq.map (fun x -> x |> PoolViewMapper.mapToView)
        }
