namespace Pippel.Tyche.Bet.Data.Models

open Pippel.Tyche.Bet.Type
open Pippel.Type

type PointPK = { PointID: Uuid }

type PointDomain =
    { ID: PointPK
      WinOrDrawPoint: ScoreWeight
      HomeResultPoint: ScoreWeight
      AwayResultPoint: ScoreWeight
      DifferencePoint: ScoreWeight
      InvertedDifferencePoint: ScoreWeight }
