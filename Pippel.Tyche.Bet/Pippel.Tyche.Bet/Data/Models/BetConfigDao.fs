namespace Pippel.Tyche.Bet.Data.Models

open System

[<CLIMutable>]
type BetConfigDao =
    { ID: Guid
      HomeResultPoint: int32
      AwayResultPoint: int32
      DiferencePoint: int32
      InvertedDiferentePoint: int32
      MatchID: Guid }
            