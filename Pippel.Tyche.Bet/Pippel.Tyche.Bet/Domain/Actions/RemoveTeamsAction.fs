namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type RemoveTeamsAction(teamRepository: ITeamRepository, unitOfWork: IUnitOfWork, teamMapper: IMapper<Team, TeamDao>) =
    inherit RemoveAction<TeamDao, Team>(teamRepository, unitOfWork, teamMapper)

    override this.AsyncExecute(key: obj [] seq): Async<Team seq> =
        base.AsyncExecute
            (key
             |> Seq.map (fun x ->
                 x
                 |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj)))
