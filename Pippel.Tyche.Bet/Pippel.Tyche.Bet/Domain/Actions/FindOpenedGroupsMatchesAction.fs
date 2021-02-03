namespace Pippel.Tyche.Bet.Actions

open System
open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

type FindOpenedGroupsMatchesAction(repository: IGroupMatchRepository, mapper: IMapper<GroupMatch, GroupMatchDao>) =
    inherit FindAction<GroupMatchDao, GroupMatch>(repository, mapper)

    member this.AsyncExecute(gamblerID: Uuid): Async<GroupMatch seq> =
        async {
            let gamblerID = gamblerID |> Uuid.value
            let now = DateTime.Now

            let! items =
                repository.AsyncFind
                    (ExpressionQueryObject<GroupMatchDao>(fun x -> now >= x.StartDate && now < x.EndDate))

            return
                items
                |> Seq.map (fun x -> x :?> GroupMatchDao |> mapper.MapToSource)
        }
