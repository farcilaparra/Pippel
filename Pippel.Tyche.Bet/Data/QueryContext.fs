namespace Pippel.Tyche.Bet

open Microsoft.EntityFrameworkCore
open Pippel.Tyche.Bet.Data.Models.Queries

type QueryContext(options: DbContextOptions<QueryContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private _MatchesGamblersView: DbSet<PoolReviewViewDao>

    member this.MatchesGamblersView
        with get () = this._MatchesGamblersView
        and set v = this._MatchesGamblersView <- v

    [<DefaultValue>]
    val mutable private _MatchesView: DbSet<MatchViewDao>

    member this.MatchesView
        with get () = this._MatchesGamblersView
        and set v = this._MatchesGamblersView <- v

    [<DefaultValue>]
    val mutable private _MatchGroupsView: DbSet<MasterPoolMatchViewDao>

    member this.MatchGroupsView
        with get () = this._MatchGroupsView
        and set v = this._MatchGroupsView <- v

    [<DefaultValue>]
    val mutable private _BetsPositionsView: DbSet<BetPositionViewDao>

    member this.BetsPositionsView
        with get () = this._BetsPositionsView
        and set v = this._BetsPositionsView <- v

    [<DefaultValue>]
    val mutable private _OnPlayingMatchesView: DbSet<OnPlayingMatchViewDao>

    member this.OnPlayingMatchesView
        with get () = this._OnPlayingMatchesView
        and set v = this._OnPlayingMatchesView <- v

    override this.OnModelCreating builder =
        builder.ApplyConfiguration(PoolReviewViewEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(MatchViewEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(MasterPoolMatchViewEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(BetPositionViewEntityTypeConfiguration())
        |> ignore

        builder.ApplyConfiguration(OnPlayingMatchViewEntityTypeConfiguration())
        |> ignore
