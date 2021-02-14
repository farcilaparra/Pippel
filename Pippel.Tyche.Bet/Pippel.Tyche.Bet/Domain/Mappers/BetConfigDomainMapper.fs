namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type BetConfigDomainMapper() =
    interface IMapper<BetConfig, BetConfigDao> with

        /// <summary>Maps from <c>BetConfig</c> to <c>BetConfigDao</c></summary>
        member this.Map(betConfig: BetConfig): BetConfigDao =
            { BetConfigDao.ID = betConfig.ID |> Uuid.toGuid
              HomeResultPoint = betConfig.HomeResultPoint |> PositiveInt.value
              AwayResultPoint = betConfig.AwayResultPoint |> PositiveInt.value
              DiferencePoint = betConfig.DiferencePoint |> PositiveInt.value
              InvertedDiferentePoint = betConfig.InvertedDiferentePoint |> PositiveInt.value
              MatchID = betConfig.MatchID |> Uuid.toGuid }

        /// <summary>Maps from <c>VatDao</c> to <c>Vat</c></summary>
        member this.Map(betConfigDao: BetConfigDao): BetConfig =
            { BetConfig.ID = betConfigDao.ID |> Uuid.createFromGuid
              HomeResultPoint = betConfigDao.HomeResultPoint |> PositiveInt.create
              AwayResultPoint = betConfigDao.AwayResultPoint |> PositiveInt.create
              DiferencePoint = betConfigDao.DiferencePoint |> PositiveInt.create
              InvertedDiferentePoint = betConfigDao.InvertedDiferentePoint |> PositiveInt.create
              MatchID = betConfigDao.MatchID |> Uuid.createFromGuid }
