namespace Pippel.Tyche.Bet

open Microsoft.EntityFrameworkCore
open Pippel.Tyche.Bet.Data.Models

type Context(options: DbContextOptions<Context>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private _GroupsBets: DbSet<PoolDao>

    member this.Pools
        with get () = this._GroupsBets
        and set v = this._GroupsBets <- v

    [<DefaultValue>]
    val mutable private _Gamblers: DbSet<GamblerDao>

    member this.Gamblers
        with get () = this._Gamblers
        and set v = this._Gamblers <- v

    [<DefaultValue>]
    val mutable private _GroupsBetsGamblers: DbSet<PoolEnrollmentDao>

    member this.GroupsBetsGamblers
        with get () = this._GroupsBetsGamblers
        and set v = this._GroupsBetsGamblers <- v

    [<DefaultValue>]
    val mutable private _Bets: DbSet<BetDao>

    member this.Bets
        with get () = this._Bets
        and set v = this._Bets <- v

    [<DefaultValue>]
    val mutable private _GroupMatchs: DbSet<MasterPoolDao>

    member this.MasterPools
        with get () = this._GroupMatchs
        and set v = this._GroupMatchs <- v

    [<DefaultValue>]
    val mutable private _RoundMatchs: DbSet<RoundDao>

    member this.RoundMatchs
        with get () = this._RoundMatchs
        and set v = this._RoundMatchs <- v

    [<DefaultValue>]
    val mutable private _Matches: DbSet<MatchDao>

    member this.Matches
        with get () = this._Matches
        and set v = this._Matches <- v

    [<DefaultValue>]
    val mutable private _Teams: DbSet<TeamDao>

    member this.Teams
        with get () = this._Teams
        and set v = this._Teams <- v

    [<DefaultValue>]
    val mutable private _RoundsMatchesConfigs: DbSet<PointDao>

    member this.RoundsMatchesConfigs
        with get () = this._RoundsMatchesConfigs
        and set v = this._RoundsMatchesConfigs <- v

    override this.OnModelCreating builder =
        builder.ApplyConfiguration(PoolEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(GamblerEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(PoolEnrollmentEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(BetEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(MasterPoolEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(RoundEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(MatchEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(TeamEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(PointEntityTypeConfiguration())
        |> ignore
