namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindBetByKeyAction(betRepository: IBetRepository, betMapper: IMapper<Bet, BetDao>) =
    inherit FindByKeyAction<BetDao, Bet>(betRepository, betMapper)

    override this.AsyncExecute(key: obj []): Async<Bet> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
