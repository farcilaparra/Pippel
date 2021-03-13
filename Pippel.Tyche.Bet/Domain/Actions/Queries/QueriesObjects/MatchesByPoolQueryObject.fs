namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type MatchesByPoolQueryObject(groupBetID: Uuid) =

    interface IQueryObject with

        member this.Query(query: IQueryable) : IQueryable =
            let groupBetID = groupBetID |> Uuid.value

            (query :?> IQueryable<MatchViewDao>)
                .Where(fun x -> x.PoolID = groupBetID)
                .OrderBy(fun x -> x.MatchDate)
            :> IQueryable
