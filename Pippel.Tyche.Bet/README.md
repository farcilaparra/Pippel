# Project that manage the domain of bets

This project let to manage the domain and the persistence of bets.

## Models

### `Bet`

```f#
type BetPK =
    { PoolID: Uuid
      GamblerID: Uuid
      MatchID: Uuid }

type BetDomain =
    { ID: BetPK
      HomeTeamValue: PositiveInt
      AwayTeamValue: PositiveInt }
```

### `Gambler`

```f#
type GamblerPK =
    { UserID: Uuid }

type GamblerDomain =
    { ID: GamblerPK }
```

### `Pool`

```f#
type PoolPK =
    { PoolID: Uuid }

type PoolDomain =
    { ID: PoolPK
      GroupMatchID: Uuid
      OwnerGamblerID: Uuid
      CreationDate: DateTime }
```

### `PoolEnrollment`

```f#
type PoolEnrollmentPK =
    { PoolID: Uuid
      GamblerID: Uuid }

type PoolEnrollmentDomain =
    { ID: PoolEnrollmentPK
      EnrollmentDate: DateTime }
```

### `MasterPool`

```f#
type MasterPoolPK =
    { MasterPoolID: Uuid }

type MasterPoolDomain =
    { ID: MasterPoolPK
      Name: NotEmptyString
      StartDate: DateTime
      EndDate: DateTime }
```

### `Match`

```f#
type MatchPK =
    { MatchID: Uuid }

type MatchDomain =
    { ID: MatchPK
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: PositiveInt option
      AwayResult: PositiveInt option
      Status: MatchStatus }
```

### `Point`

```f#
type PointPK =
    { PointID: Uuid }

type PointDomain =
    { ID: PointPK
      WinOrDrawPoint: PositiveInt
      HomeResultPoint: PositiveInt
      AwayResultPoint: PositiveInt
      DiferencePoint: PositiveInt
      InvertedDiferentePoint: PositiveInt }
```

### `Round`

```f#
type RoundPK =
    { RoundID: Uuid }

type RoundDomain =
    { ID: RoundPK
      MasterPoolID: Uuid
      Name: NotEmptyString
      PointID: Uuid }
```

### `Team`

```f#
type TeamPK =
    { TeamID: Uuid }

type TeamDomain =
    { ID: TeamPK
      Name: NotEmptyString }
```

## Actions

### `AddBetsAction`

Marks several `Bet` to persist.

### `EditBetAction`

Saves several `Bet`.

### `FindBetByKeyAction`

Finds a `Bet` by its primary key.

### `FindPoolByKeyAction`

Finds a `Pool` by its primary key.

### `FindBetByKeyAction`

Finds a `Bet` by its primary key.

### `FindMatchByKeyAction`

Finds a `Match` by its primary key.

### `UpdateBetsAction`

Marks several `Bet` to update.
