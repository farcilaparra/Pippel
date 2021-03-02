namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type TeamPK = { TeamID: Uuid }

type TeamDomain =
    { ID: TeamPK
      Name: NotEmptyString }
