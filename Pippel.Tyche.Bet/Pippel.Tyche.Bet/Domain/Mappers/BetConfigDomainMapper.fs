namespace Pippel.Tyche.Bet.Domain.Mappers

open Pippel.Core
open Pippel.Tyche.Bet.Domain.Models
open Pippel.Tyche.Bet.Data.Models
open Pippel.Type

type BetConfigDomainMapper() =
    interface IMapper<BetConfig, BetConfigDao> with

        /// <summary>Maps from <c>BetConfig</c> to <c>BetConfigDao</c></summary>
        member this.MapToTarget(betConfig: BetConfig): BetConfigDao =
            { BetConfigDao.ID = betConfig.ID |> Uuid.toGuid
              HomeResultPoint = betConfig.HomeResultPoint |> PositiveInt32.value
              AwayResultPoint = betConfig.AwayResultPoint |> PositiveInt32.value
              DiferencePoint = betConfig.DiferencePoint |> PositiveInt32.value
              InvertedDiferentePoint = betConfig.InvertedDiferentePoint |> PositiveInt32.value
              MatchID = betConfig.MatchID |> Uuid.toGuid }

        /// <summary>Maps from <c>VatDao</c> to <c>Vat</c></summary>
        member this.MapToSource(betConfigDao: BetConfigDao): BetConfig =
            { BetConfig.ID = betConfigDao.ID |> Uuid.createFromGuid
              HomeResultPoint = betConfigDao.HomeResultPoint |> PositiveInt32.create
              AwayResultPoint = betConfigDao.AwayResultPoint |> PositiveInt32.create
              DiferencePoint = betConfigDao.DiferencePoint |> PositiveInt32.create
              InvertedDiferentePoint = betConfigDao.InvertedDiferentePoint |> PositiveInt32.create
              MatchID = betConfigDao.MatchID |> Uuid.createFromGuid }
