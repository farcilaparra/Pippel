# REST service to manage bets

## BET

### GET `/bet/openend?gamblerid=*`

Returns the opened group match for a gambler.

#### Request

`/bet/opened?gamblerid=9798ff07-f9d5-462d-acb1-bd58b553ff2e&skip=0&take=10&filter=mundial`

#### Response

```json
{
    "currentPage": 0,
    "pageCount": 1,
    "pageSize": 10,
    "itemsCount": 1,
    "items": [
        {
            "poolID": "1be04ebd-bb83-1d2f-e053-020011ac624c",
            "gamblerID": "49a849bd-b8ba-bc01-e053-020011acd428",
            "masterPoolName": "Eliminatorias sudaméricanas al mundial Qatar 2024 marzo de 2021",
            "currentPosition": null,
            "beforePosition": null
        }
    ]
}
```

## POOL

### POST `/pool`

Add several pools to persist.

#### Request

```json
[
    {
        "poolID": null,
        "masterPoolID": "863551BE-EA38-CF04-E053-020011ACAF9E",
        "OwnerGamblerID": "49A849BD-B8BA-BC01-E053-020011ACD428",
        "name": "Copa América de Felipe 2021"
    }
]
```

#### Response

```json
[
    {
        "poolID": "1a55e98d-7a26-412d-b15a-35a5e75cee2e",
        "masterPoolID": "863551be-ea38-cf04-e053-020011acaf9e",
        "ownerGamblerID": "49a849bd-b8ba-bc01-e053-020011acd428",
        "name": "Copa América de Felipe 2021"
    }
]
```
