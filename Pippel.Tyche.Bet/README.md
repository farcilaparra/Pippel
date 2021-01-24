# Project that manage the domain of bets

This project let to manage the domain and the persistence of bets.

## IAppUserRepository

It's an interface with the actions over the ```AppUserDao```.

```f#
[<Interface>]
type IAppUserRepository =
    inherit IRepository<AppUserDao>
```

## IBetRepository

It's an interface with the actions over the ```BetDao```.

```f#
[<Interface>]
type IBetRepository =
    inherit IRepository<BetDao>
```

## IBetConfigRepository

It's an interface with the actions over the ```BetConfigDao```.

```f#
[<Interface>]
type IBetConfigRepository =
    inherit IRepository<BetConfigDao>
```

## IGamblerRepository

It's an interface with the actions over the ```GamblerDao```.

```f#
[<Interface>]
type IGamblerRepository =
    inherit IRepository<GamblerDao>
```

## IGroupBetRepository

It's an interface with the actions over the ```GroupBetDao```.

```f#
[<Interface>]
type IGroupBetRepository =
    inherit IRepository<GroupBetDao>
```

## IGroupBetGamblerRepository

It's an interface with the actions over the ```GroupBetGamblerDao```.

```f#
[<Interface>]
type IGroupBetGamblerRepository =
    inherit IRepository<GroupBetGamblerDao>
```

## IGroupMatchRepository

It's an interface with the actions over the ```GroupMatchDao```.

```f#
[<Interface>]
type IGroupMatchRepository =
    inherit IRepository<GroupMatchDao>
```

## IHistoryBetRepository

It's an interface with the actions over the ```HistoryBetDao```.

```f#
[<Interface>]
type IHistoryBetRepository =
    inherit IRepository<HistoryBetDao>
```

## IMatchRepository

It's an interface with the actions over the ```MatchDao```.

```f#
[<Interface>]
type IMatchRepository =
    inherit IRepository<MatchDao>
```

## IRoundMatchRepository

It's an interface with the actions over the ```RoundMatchDao```.

```f#
[<Interface>]
type IRoundMatchRepository =
    inherit IRepository<RoundMatchDao>
```

## ITeamRepository

It's an interface with the actions over the ```TeamDao```.

```f#
[<Interface>]
type ITeamRepository =
    inherit IRepository<TeamDao>
```