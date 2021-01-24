namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindRoundMatchByKeyAction(roundMatchRepository: IRoundMatchRepository,
                               roundMatchMapper: IMapper<RoundMatch, RoundMatchDao>) =
    inherit FindByKeyAction<RoundMatchDao, RoundMatch>(roundMatchRepository, roundMatchMapper)

    override this.AsyncExecute(key: obj []): Async<RoundMatch> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
