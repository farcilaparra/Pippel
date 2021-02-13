namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindGamblerByKeyAction(gamblerRepository: IGamblerRepository, gamblerMapper: IMapper<Gambler, GamblerDao>) =
    inherit FindByKeyAction<GamblerDao, Gambler>(gamblerRepository, gamblerMapper)

    override this.AsyncExecute(key: obj []): Async<Gambler> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
