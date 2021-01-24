namespace Pippel.Tyche.Bet.Actions

open Pippel.Core
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindTeamByKeyAction(teamRepository: ITeamRepository, teamMapper: IMapper<Team, TeamDao>) =
    inherit FindByKeyAction<TeamDao, Team>(teamRepository, teamMapper)

    override this.AsyncExecute(key: obj []): Async<Team> =
        base.AsyncExecute
            (key
             |> Array.map (fun x -> (x :?> Uuid |> Uuid.toGuid) :> obj))
