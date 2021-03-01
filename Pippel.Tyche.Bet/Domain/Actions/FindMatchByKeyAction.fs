namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Mappers
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories
open Pippel.Data.Actions

type FindMatchByKeyAction(matchRepository: IMatchRepository) =

    interface IFindMatchByKeyAction with

        member this.AsyncExecute(id: MatchPK) : Async<MatchDomain> =
            matchRepository
            |> asyncFindByKey [| id.MatchID |] MatchDomainMapper.mapToDomain
