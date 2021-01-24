namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindMatchByKeyAction(matchRepository: IMatchRepository, matchMapper: IMapper<Match, MatchDao>) =
    inherit FindByKeyAction<MatchDao, Match>(matchRepository, matchMapper)

    override this.AsyncExecute(key: obj []): Async<Match> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
