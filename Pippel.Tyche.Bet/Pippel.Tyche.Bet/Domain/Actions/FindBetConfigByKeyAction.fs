namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindBetConfigByKeyAction(betConfigRepository: IBetConfigRepository,
                              betConfigMapper: IMapper<BetConfig, BetConfigDao>) =
    inherit FindByKeyAction<BetConfigDao, BetConfig>(betConfigRepository, betConfigMapper)

    override this.AsyncExecute(key: obj []): Async<BetConfig> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
