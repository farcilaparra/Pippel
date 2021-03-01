namespace Pippel.Tyche.Bet.Domain.Actions

open Pippel.Tyche.Bet.Domain.Models

[<Interface>]
type IFindPoolByKeyAction =

    abstract AsyncExecute : PoolPK -> Async<PoolDomain>
