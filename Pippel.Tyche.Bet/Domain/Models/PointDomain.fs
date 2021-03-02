namespace Pippel.Tyche.Bet.Data.Models

open Pippel.Type

type PointPK = { PointID: Uuid }

type PointDomain =
    { ID: PointPK
      WinOrDrawPoint: PositiveInt
      HomeResultPoint: PositiveInt
      AwayResultPoint: PositiveInt
      DiferencePoint: PositiveInt
      InvertedDiferentePoint: PositiveInt }
