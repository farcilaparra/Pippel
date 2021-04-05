# Project that manage the domain of bets

This project let to manage the domain and the persistence of the bets.

## Models

### `Bet`

```f#
type BetPK =
    { PoolID: Uuid
      GamblerID: Uuid
      MatchID: Uuid }

type BetDomain =
    { ID: BetPK
      HomeTeamValue: Score
      AwayTeamValue: Score }
```

### `Gambler`

```f#
type GamblerPK = { UserID: Uuid }

type GamblerDomain = { ID: GamblerPK }
```

### `Pool`

```f#
type PoolDomain =
    { ID: PoolPK
      MasterPoolID: Uuid
      OwnerGamblerID: Uuid
      CreationDate: DateTime
      Name: NotEmptyString100 }
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
type PoolEnrollmentPK =
    { PoolID: Uuid
      GamblerID: Uuid }

type PoolEnrollmentDomain =
    { ID: PoolEnrollmentPK
      EnrollmentDate: DateTime }
```

### `Match`

```f#
type MatchPK = { MatchID: Uuid }

type MatchDomain =
    { ID: MatchPK
      HomeTeamID: Uuid
      AwayTeamID: Uuid
      RoundMatchID: Uuid
      MatchDate: DateTime
      HomeResult: Score option
      AwayResult: Score option
      Status: MatchStatus }
```

### `Point`

```f#
type PointDomain =
    { ID: PointPK
      WinOrDrawPoint: ScoreWeight
      HomeResultPoint: ScoreWeight
      AwayResultPoint: ScoreWeight
      DifferencePoint: ScoreWeight
      InvertedDifferencePoint: ScoreWeight }
```

### `Round`

```f#
type RoundPK = { RoundID: Uuid }

type RoundDomain =
    { ID: RoundPK
      MasterPoolID: Uuid
      Name: NotEmptyString100
      PointID: Uuid }
```

### `Team`

```f#
type TeamPK = { TeamID: Uuid }

type TeamDomain =
    { ID: TeamPK
      Name: NotEmptyString100 }
```

## Actions

### `AddBetsAction`

Marks several `Bet` to persist.

### `AddPoolsAction`

Marks several `Pools` to persist.

### `EditBetAction`

Saves several `Bet`.

### `FindBetByKeyAction`

Finds a `Bet` by its primary key.

### `FindMatchByKeyAction`

Finds a `Match` by its primary key.

### `FindPoolByKeyAction`

Finds a `Pool` by its primary key.

### `FindPoolEnrollmentByKeyAction`

Finds a `PoolEnrollment` by its primary key.

### `UpdateBetsAction`

Marks several `Bet` to update.
