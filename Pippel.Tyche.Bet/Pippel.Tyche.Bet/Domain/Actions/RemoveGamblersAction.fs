namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveGamblersAction(gamblerRepository: IGamblerRepository,
                          unitOfWork: IUnitOfWork,
                          gamblerMapper: IMapper<Gambler, GamblerDao>) =
    inherit RemoveAction<GamblerDao, Gambler>(gamblerRepository, unitOfWork, gamblerMapper)

    override this.AsyncExecute(key: obj [] seq): Async<Gambler seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
