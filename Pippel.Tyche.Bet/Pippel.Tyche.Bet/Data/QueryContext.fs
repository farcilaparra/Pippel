namespace Pippel.Tyche.Bet

open Microsoft.EntityFrameworkCore
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Entities.Configurations.Queries

type QueryContext(options: DbContextOptions<QueryContext>) =
    inherit DbContext(options)
            
    [<DefaultValue>]
    val mutable private _MatchesGamblersView: DbSet<MatchGamblerViewDao>
    
    member this.MatchesGamblersView
        with get () = this._MatchesGamblersView
        and set v = this._MatchesGamblersView <- v
        
    [<DefaultValue>]
    val mutable private _MatchesView: DbSet<MatchViewDao>
    
    member this.MatchesView
        with get () = this._MatchesGamblersView
        and set v = this._MatchesGamblersView <- v

    override this.OnModelCreating builder =
        builder.ApplyConfiguration(MatchGamblerViewEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(MatchViewEntityTypeConfiguration()) |> ignore
