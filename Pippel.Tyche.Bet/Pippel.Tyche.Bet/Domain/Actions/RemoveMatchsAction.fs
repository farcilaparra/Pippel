namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveMatchsAction(matchRepository: IMatchRepository,
                        unitOfWork: IUnitOfWork,
                        matchMapper: IMapper<Match, MatchDao>) =
    inherit RemoveAction<MatchDao, Match>(matchRepository, unitOfWork, matchMapper)

    override this.AsyncExecute(key: obj [] seq): Async<Match seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
