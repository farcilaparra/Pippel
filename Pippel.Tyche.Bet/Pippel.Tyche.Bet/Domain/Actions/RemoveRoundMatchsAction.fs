namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveRoundMatchsAction(roundMatchRepository: IRoundMatchRepository,
                             unitOfWork: IUnitOfWork,
                             roundMatchMapper: IMapper<RoundMatch, RoundMatchDao>) =
    inherit RemoveAction<RoundMatchDao, RoundMatch>(roundMatchRepository, unitOfWork, roundMatchMapper)

    override this.AsyncExecute(key: obj [] seq): Async<RoundMatch seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
