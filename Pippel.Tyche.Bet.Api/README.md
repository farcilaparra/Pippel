# REST service to manage bets

## BET

### GET `/bet/openend?gamblerid=*`

Returns the opened group match for a gambler.

#### Request

`/bet/opened?gamblerid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "GroupBetID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GroupMatchName": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "CurrentPoint": 15,
   "CurrentPosition": 2,
   "BeforePosition": 3 }]
```

### GET `/bet/matches?groupbetid=*`

Returns the matches filtered by group bet id.

#### Request

`/bet/matches?groupbetid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "RoundMatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GroupMatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GroupBetID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
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
{ "GroupBetID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
  "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
  "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
  "HomeTeamValue": 2,
  "AwayTeamValue": 1 }
```

#### Response

```json
[{ "GroupBetID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "HomeTeamValue": 2,
   "AwayTeamValue": 1 }]
```

### GET `/bet/position?groupbetid=*`

Returns the bet's position filtered by group bet id.

#### Request

`/bet/position?groupbetid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "GroupBetID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "GamblerID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "EnrollmentDate": "",
   "Point": null,
   "CurrentPosition": null,
   "BeforePosition": null }]
```

### GET `/bet/positionandonplayingmatches?groupbetid=*`

Returns the bet's positions and the matches is on playing filtered by group bet id.

#### Request

`/bet/positionandonplayingmatches?groupbetid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
{ "BetsPositions": [
    { "GroupBetID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
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
      "GroupMatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e" }]}
```

## Match

### GET `/match?groupmatchid=*`

Returns the matches filtered by group match id.

#### Request

`/match?groupmatchid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "GroupMatchId": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "HomeTeamName": "Colombia",
   "AwayTeamName": "Brasil",
   "MatchDate": "" }]
```

### GET `/match/onplaying?groupbetid=*`

Returns the opened group match for a gambler.

#### Request

`/match/onplaying?groupmatchid=9798ff07-f9d5-462d-acb1-bd58b553ff2e`

#### Response

```json
[{ "MatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "HomeTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "AwayTeamID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
   "MatchDate": "",
   "HomeTeamName": "Colombia",
   "AwayTeamName": "Brasil",
   "GroupMatchID": "9798ff07-f9d5-462d-acb1-bd58b553ff2e" }]
```
