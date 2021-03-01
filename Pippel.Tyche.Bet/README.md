# Project that manage the domain of bets

This project let to manage the domain and the persistence of bets.

## Models

### `Bet`

```f#
type BetDomain =
    { GroupBetID: Uuid
      GamblerID: Uuid
      MatchID: Uuid
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt }
```

### `Gambler`

```f#
type GamblerDomain = { UserID: Uuid }
```

### `GroupBet`

```f#
type GroupBetDomain =
    { ID: Uuid
      GroupMatchID: Uuid
      OwnerGamblerID: Uuid
      CreationDate: DateTime }
```

### `GroupBetGambler`

```f#
type GroupBetGamblerDomain =
    { GroupBetID: Uuid
      GamblerID: Uuid
      Role: GamblerRole
      EnrollmentDate: DateTime }
```

### `GroupMatch`

```f#
type GroupMatchDomain =
    { ID: Uuid
      Name: NonEmptyString
      StartDate: DateTime
      EndDate: DateTime }
```

### `HistoryBet`

```f#
type HistoryBetDomain =
    { ID: Uuid
      GroupBetID: Uuid
      GamblerID: Uuid
      MatchID: Uuid
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt
      CreationDate: DateTime }
```

### `Match`

```f#
type MatchDomain =
    { ID: Uuid
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt option
      AwayResult: PositiveInt option
      Status: MatchStatus }
```

### `RoundMatchConfig`

```f#
type RoundMatchConfigDomain =
    { RoundMatchID: Uuid
      HomeResultPoint: PositiveInt
      AwayResultPoint: PositiveInt
      DiferencePoint: PositiveInt
      InvertedDiferentePoint: PositiveInt
      WinOrDrawPoint: PositiveInt }
```

### `Bet`

```f#
type RoundMatchDomain =
    { ID: Uuid
      GroupMatchID: Uuid
      Name: NonEmptyString
      Config: RoundMatchConfigDomain }
```

### `Team`

```f#
type TeamDomain = { ID: Uuid; Name: NonEmptyString }
```

## Actions

### `AddBetsAction`

Marks several `Bet` to persist. 

### `AddHistoryBetsAction`

Marks several `HistoryBet` to persist.

### `EditBetAction`

Saves several `Bet`.

### `FindBetByKeyAction`

Finds a `Bet` by its primary key.

### `FindGroupBetByKeyAction`

Finds a `GroupBet` by its primary key.

### `FindGroupBetByKeyAction`

Marks several `Bet` to persist.

### `FindMatchByKeyAction`

Finds a `Match` by its primary key.

### `UpdateBetsAction`

Marks several `Bet` to update.
