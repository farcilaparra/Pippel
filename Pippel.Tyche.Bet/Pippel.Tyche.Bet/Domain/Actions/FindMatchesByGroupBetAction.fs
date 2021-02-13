namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type
open Pippel.Tyche.Bet.Domain.Actions.Queries

type FindMatchesByGroupBetAction(repository: IQueryRepository<MatchViewDao>) =

    interface IFindMatchesByGroupBetAction with
    
        member this.AsyncExecute(groupBetID: Uuid): Async<MatchViewDao seq> =
            async {
                let! items = repository.AsyncFind(MatchesByGroupBetQueryObject(groupBetID |> Uuid.toGuid))

                return
                    items
                    |> Seq.map (fun x -> x :?> MatchViewDao)
            }
