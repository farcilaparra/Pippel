namespace Pippel.Tyche.Bet.Domain.Actions.Queries.QueriesObjects

open System.Linq
open Pippel.Data
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Type

type MatchesByMasterPoolQueryObject(groupMatchID: Uuid) =

    interface IQueryObject with

        member this.Query(query: IQueryable) : IQueryable =
            let groupMatchID = groupMatchID |> Uuid.value

            (query :?> IQueryable<MasterPoolMatchViewDao>)
                .Where(fun x -> x.MasterPoolID = groupMatchID)
                .OrderBy(fun x -> x.MatchDate)
            :> IQueryable
