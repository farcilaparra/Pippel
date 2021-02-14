namespace Pippel.Tyche.Bet

open Microsoft.EntityFrameworkCore
open Pippel.Tyche.Bet.Data.Models.Queries
open Pippel.Tyche.Bet.Entities.Configurations
open Pippel.Tyche.Bet.Entities.Configurations.Queries

type QueryContext(options: DbContextOptions<QueryContext>) =
    inherit DbContext(options)
            
    [<DefaultValue>]
    val mutable private _MatchesGamblersView: DbSet<MatchGroupGamblerViewDao>
    
    member this.MatchesGamblersView
        with get () = this._MatchesGamblersView
        and set v = this._MatchesGamblersView <- v
        
    [<DefaultValue>]
    val mutable private _MatchesView: DbSet<MatchViewDao>
    
    member this.MatchesView
        with get () = this._MatchesGamblersView
        and set v = this._MatchesGamblersView <- v

    [<DefaultValue>]
    val mutable private _MatchGroupsView: DbSet<MatchGroupViewDao>
    
    member this.MatchGroupsView
        with get () = this._MatchGroupsView
        and set v = this._MatchGroupsView <- v
        
    [<DefaultValue>]
    val mutable private _BetsPositionsView: DbSet<BetPositionViewDao>
    
    member this.BetsPositionsView
        with get () = this._BetsPositionsView
        and set v = this._BetsPositionsView <- v

    override this.OnModelCreating builder =
        builder.ApplyConfiguration(MatchGroupGamblerViewEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(MatchViewEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(MatchGroupViewEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(BetPositionViewEntityTypeConfiguration()) |> ignore
