namespace Pippel.Tyche.Bet.Data.Models

open System
            
[<CLIMutable>]
type AppUserDao =
    { ID: Guid
      Email: String
      Password: String}
