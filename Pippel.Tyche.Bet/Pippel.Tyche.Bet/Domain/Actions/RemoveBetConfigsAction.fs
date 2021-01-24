namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveBetConfigsAction(betConfigRepository: IBetConfigRepository,
                            unitOfWork: IUnitOfWork,
                            betConfigMapper: IMapper<BetConfig, BetConfigDao>) =
    inherit RemoveAction<BetConfigDao, BetConfig>(betConfigRepository, unitOfWork, betConfigMapper)

    override this.AsyncExecute(key: obj [] seq): Async<BetConfig seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
