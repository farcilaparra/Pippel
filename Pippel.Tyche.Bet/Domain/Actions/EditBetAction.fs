namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

type EditBetAction
    (
        findPoolEnrollmentByKeyAction: IFindPoolEnrollmentByKeyAction,
        findMatchByKeyAction: IFindMatchByKeyAction,
        findBetByKeyAction: IFindBetByKeyAction,
        updateBetsAction: IUpdateBetsAction,
        addBetsAction: IAddBetsAction
    ) =

    let asyncUpdateBet (betDomain: BetDomain) =
        async {
            let! currentBetDomain =
                findBetByKeyAction.AsyncExecute
                    { PoolID = betDomain.ID.PoolID
                      GamblerID = betDomain.ID.GamblerID
                      MatchID = betDomain.ID.MatchID }

            let newBetDomain =
                { currentBetDomain with
                      HomeTeamValue = betDomain.HomeTeamValue
                      AwayTeamValue = betDomain.AwayTeamValue }

            let! updatedBetsDomain = updateBetsAction.AsyncExecute([| newBetDomain |])
            return updatedBetsDomain
        }

    let asyncAddBet (betDomain: BetDomain) =
        async { return! addBetsAction.AsyncExecute([| betDomain |]) }

    let asyncAddOrUpdateBet (betDomain: BetDomain) =
        async {
            try
                return! asyncUpdateBet betDomain
            with :? NotFoundException -> return! asyncAddBet betDomain
        }

    let asyncRaiseExceptionIfMatchNoExist (matchID: Uuid) =
        async {
            let! currentMatchDomain = findMatchByKeyAction.AsyncExecute { MatchID = matchID }

            if currentMatchDomain.Status <> MatchStatus.Pending then
                raise
                <| EditingBetNotAllowedException("The match can't edit because its status isn't pending")

            return currentMatchDomain
        }

    let asyncRaiseExceptionIfGropBetGamblerNoExist (groupBetID: Uuid) (gamblerID: Uuid) =
        async {
            return!
                findPoolEnrollmentByKeyAction.AsyncExecute
                    { PoolID = groupBetID
                      GamblerID = gamblerID }
        }

    let asyncEdit (betDomain: BetDomain) =
        async {

            let! currentMatchDomain = asyncRaiseExceptionIfMatchNoExist betDomain.ID.MatchID

            let! currentGroupBetGamblerDomain =
                asyncRaiseExceptionIfGropBetGamblerNoExist betDomain.ID.PoolID betDomain.ID.GamblerID

            return! asyncAddOrUpdateBet betDomain
        }

    interface IEditBetAction with

        member this.AsyncExecute(betsDomain: BetDomain seq) =
            async {
                let mutable editedBetsDomain : BetDomain list = []

                for betDomain in betsDomain do
                    let! currentEditedBetsDomain = asyncEdit betDomain

                    editedBetsDomain <-
                        (currentEditedBetsDomain |> Seq.toList)
                        @ editedBetsDomain

                return editedBetsDomain |> List.toSeq
            }
