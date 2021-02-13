namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Core
open Pippel.Data
open Pippel.Tyche.Bet
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Type

type EditBetAction(findGroupBetGamblerByKeyAction: IFindGroupBetGamblerByKeyAction,
                   findMatchByKeyAction: IFindMatchByKeyAction,
                   findBetByKeyAction: IFindBetByKeyAction,
                   updateBetsAction: IUpdateBetsAction,
                   addBetsAction: IAddBetsAction,
                   unitOfWork: IUnitOfWork) =

    let asyncUpdateBet (editingBet: EditingBet) =
        async {
            let! currentBet =
                findBetByKeyAction.AsyncExecute
                    ([| editingBet.GroupBetID
                        editingBet.GamblerID
                        editingBet.MatchID |])
            
            let newBet =
                { currentBet with
                      HomeTeamValue = editingBet.HomeValue
                      AwayTeamValue = editingBet.AwayValue }

            let! updatedBet = updateBetsAction.AsyncExecute([| newBet |])
            return updatedBet
        }

    let asyncAddBet (editingBet: EditingBet) =
        async {
            let newBet =
                { Bet.ID = Uuid.newUuid ()
                  GroupBetID = editingBet.GroupBetID
                  GamblerID = editingBet.GamblerID
                  MatchID = editingBet.MatchID
                  HomeTeamValue = editingBet.HomeValue
                  AwayTeamValue = editingBet.AwayValue }

            let! addedBet = addBetsAction.AsyncExecute([| newBet |])
            return addedBet
        }

    let asyncAddOrUpdateBet (editingBet: EditingBet) =
        async {
            try
                let! updatedBets = asyncUpdateBet editingBet
                return updatedBets
            with :? NotFoundException ->
                let! addedBets = asyncAddBet editingBet
                return addedBets
        }

    let asyncRaiseExceptionIfMatchNoExist (matchID: Uuid) =
        async {
            let! currentMatch = findMatchByKeyAction.AsyncExecute [| matchID |]
            
            if currentMatch.Status <> MatchStatus.Pending then
                raise <| EditingBetNotAllowedException("The match can't edit because its status isn't pending")
            
            return currentMatch
        }

    let asyncRaiseExceptionIfGropBetGamblerNoExist (groupBetID: Uuid) (gamblerID: Uuid) =
        async {
            let! groupBetGambler =
                findGroupBetGamblerByKeyAction.AsyncExecute [| groupBetID
                                                               gamblerID |]

            return groupBetGambler
        }

    let asyncEdit editingBet =
        async {

            let! currentMatch = asyncRaiseExceptionIfMatchNoExist editingBet.MatchID

            let! currentGroupBetGambler =
                asyncRaiseExceptionIfGropBetGamblerNoExist editingBet.GroupBetID editingBet.GamblerID

            let! editedBet = asyncAddOrUpdateBet editingBet

            return editedBet
        }

    interface IEditBetAction with
    
        member this.AsyncExecute(editingBets: EditingBet seq) =
            async {
                let mutable editedBets: Bet list = []

                for editingBet in editingBets do
                    let! editedBet = asyncEdit editingBet
                    editedBets <- (editedBet |> Seq.toList) @ editedBets

                unitOfWork.SaveChanges() |> ignore

                return editedBets |> List.toSeq
            }
