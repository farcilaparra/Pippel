namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveBetsAction(betRepository: IBetRepository, unitOfWork: IUnitOfWork, betMapper: IMapper<Bet, BetDao>) =
    inherit RemoveAction<BetDao, Bet>(betRepository, unitOfWork, betMapper)

    override this.AsyncExecute(key: obj [] seq): Async<Bet seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
