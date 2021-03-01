namespace Pippel.Tyche.Bet.Domain.Models

open Pippel.Type

type PoolEnrollmentPK =
    { PoolID: Uuid
      GamblerID: Uuid }

type PoolEnrollmentDomain =
    { ID: PoolEnrollmentPK
      EnrollmentDate: DateTime }
