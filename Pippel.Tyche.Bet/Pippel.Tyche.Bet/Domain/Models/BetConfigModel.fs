namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type BetConfig =
    { ID: Uuid
      HomeResultPoint: PositiveInt32
      AwayResultPoint: PositiveInt32
      DiferencePoint: PositiveInt32
      InvertedDiferentePoint: PositiveInt32
      MatchID: Uuid }
