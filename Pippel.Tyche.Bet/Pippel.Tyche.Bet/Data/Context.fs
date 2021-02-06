namespace Pippel.Tyche.Bet

open Microsoft.EntityFrameworkCore
open Pippel.Tyche.Bet.Data.Models
open Pippel.Tyche.Bet.Entities.Configurations

type Context(options: DbContextOptions<Context>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private _GroupsBets: DbSet<GroupBetDao>

    member this.GroupsBets
        with get () = this._GroupsBets
        and set v = this._GroupsBets <- v
        
    [<DefaultValue>]
    val mutable private _Gamblers: DbSet<GamblerDao>
        
    member this.Gamblers
        with get () = this._Gamblers
        and set v = this._Gamblers <- v
        
    [<DefaultValue>]
    val mutable private _GroupsBetsGamblers: DbSet<GroupBetGamblerDao>
        
    member this.GroupsBetsGamblers
        with get () = this._GroupsBetsGamblers
        and set v = this._GroupsBetsGamblers <- v
        
    [<DefaultValue>]
    val mutable private _Bets: DbSet<BetDao>
        
    member this.Bets
        with get () = this._Bets
        and set v = this._Bets <- v
        
    [<DefaultValue>]
    val mutable private _HistoriresBets: DbSet<HistoryBetDao>
        
    member this.HistoriresBets
        with get () = this._HistoriresBets
        and set v = this._HistoriresBets <- v

    [<DefaultValue>]
    val mutable private _GroupMatchs: DbSet<GroupMatchDao>

    member this.GroupMatchs
        with get () = this._GroupMatchs
        and set v = this._GroupMatchs <- v

    [<DefaultValue>]
    val mutable private _RoundMatchs: DbSet<RoundMatchDao>
    
    member this.RoundMatchs
        with get () = this._RoundMatchs
        and set v = this._RoundMatchs <- v
    
    [<DefaultValue>]
    val mutable private _Matchs: DbSet<MatchDao>
    
    member this.Matchs
        with get () = this._Matchs
        and set v = this._Matchs <- v

    [<DefaultValue>]
    val mutable private _Teams: DbSet<TeamDao>
    
    member this.Teams
        with get () = this._Teams
        and set v = this._Teams <- v

    [<DefaultValue>]
    val mutable private _BetConfigs: DbSet<BetConfigDao>
    
    member this.BetConfigs
        with get () = this._BetConfigs
        and set v = this._BetConfigs <- v

    [<DefaultValue>]
    val mutable private _AppUsers: DbSet<AppUserDao>
    
    member this.AppUsers
        with get () = this._AppUsers
        and set v = this._AppUsers <- v

    override this.OnModelCreating builder =
        builder.ApplyConfiguration(GroupBetEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(GamblerEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(GroupBetGamblerEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(BetEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(HistoryBetEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(GroupMatchEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(RoundMatchEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(MatchEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(TeamEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(BetConfigEntityTypeConfiguration()) |> ignore
        builder.ApplyConfiguration(AppUserEntityTypeConfiguration()) |> ignore
