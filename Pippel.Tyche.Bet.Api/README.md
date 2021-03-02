# REST service to manage bets

## BET

### GET `/bet/openend?gamblerid=*`

Returns the opened group match for a gambler.

#### Request

`/bet/opened?gamblerid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "PoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MasterPoolName": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "CurrentPoint": 15,
   "CurrentPosition": 2,
   "BeforePosition": 3 }]
```

### GET `/bet/matches?poolid=*`

Returns the matches filtered by pool id.

#### Request

`/bet/matches?poolid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "RoundID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MasterPoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "PoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MatchStatus": 1,
   "HomeTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "AwayTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MatchDate": "",
   "HomeTeamName": "Colombia",
   "AwayTeamName": "Brasil",
   "Point": null }]
```

### PUT `/bet/edit`

Saves several bets.

`/bet/edit`

#### Request

```json
{ "PoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
  "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
  "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
  "HomeTeamValue": 2,
  "AwayTeamValue": 1 }
```

#### Response

```json
[{ "PoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "HomeTeamValue": 2,
   "AwayTeamValue": 1 }]
```

### GET `/bet/position?poolid=*`

Returns the bet's position filtered by pool id.

#### Request

`/bet/position?poolid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "PoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "EnrollmentDate": "",
   "Point": null,
   "CurrentPosition": null,
   "BeforePosition": null }]
```

### GET `/bet/positionandonplayingmatches?poolid=*`

Returns the bet's positions and the matches is on playing filtered by pool id.

#### Request

`/bet/positionandonplayingmatches?groupbetid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
{ "BetsPositions": [
    { "PoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
      "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
      "EnrollmentDate": "",
      "Point": null,
      "CurrentPosition": null,
      "BeforePosition": null }],
  "OnPlayingMatches": [
    { "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
      "HomeTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
      "AwayTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
      "MatchDate": "",
      "HomeTeamName": "Colombia",
      "AwayTeamName": "Brasil",
      "MasterPoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e" }]}
```

## Match

### GET `/match?masterpoolid=*`

Returns the matches filtered by master pool id.

#### Request

`/match?masterpoolid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "MasterPoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "HomeTeamName": "Colombia",
   "AwayTeamName": "Brasil",
   "MatchDate": "" }]
```

### GET `/match/onplaying?poolid=*`

Returns the on playing matches filtered by pool id.

#### Request

`/match/onplaying?poolid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "HomeTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "AwayTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MatchDate": "",
   "HomeTeamName": "Colombia",
   "AwayTeamName": "Brasil",
   "MasterPoolID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e" }]
```
