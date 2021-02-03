namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type BetConfig =
    { ID: Uuid
      HomeResultPoint: PositiveInt
      AwayResultPoint: PositiveInt
      DiferencePoint: PositiveInt
      InvertedDiferentePoint: PositiveInt
      MatchID: Uuid }
