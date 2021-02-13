namespace Pippel.Tyche.Bet.Domain.Actions

open System
open Pippel.Core
open Pippel.Data
open Pippel.Data.Actions
open Pippel.Type
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Repositories

type AddBetsAction(betRepository: IBetRepository,
                   addHistoryBetsAction: IAddHistoryBetsAction,
                   unitOfWork: IUnitOfWork,
                   betMapper: IMapper<Bet, BetDao>) =
    inherit AddAction<BetDao, Bet>(betRepository, unitOfWork, betMapper)
    
    interface IAddBetsAction with

        override this.AsyncExecute(bets: Bet seq): Async<Bet seq> =
            let baseAsyncExecute = base.AsyncExecute bets

            async {
                let! bets = baseAsyncExecute

                let historiesBetsToAdd =
                    bets
                    |> Seq.map (fun x ->
                        { HistoryBet.ID = Uuid.newUuid ()
                          BetID = x.ID
                          HomeTeamValue = x.HomeTeamValue
                          AwayTeamValue = x.AwayTeamValue
                          CreationDate = DateTime.Now })

                let! addedHistoriesBets = addHistoryBetsAction.AsyncExecute historiesBetsToAdd

                return bets
            }
