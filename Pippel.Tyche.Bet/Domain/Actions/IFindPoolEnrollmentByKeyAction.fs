namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IFindPoolEnrollmentByKeyAction =

    abstract AsyncExecute : PoolEnrollmentPK -> Async<PoolEnrollmentDomain>
