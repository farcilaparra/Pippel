# REST service to manage taxes

## VAT

This one is the model:

```f#
type Vat =
    { ID: Uuid
      Name: NonEmptyString
      Percentage: Percentage }
```

This project has five in points:

### GET `/vat/{id}`

Returns the item that has id as primery key.

`/vat/9798ff07-f9d5-462d-acb1-bd58b553ff2e`

### GET `/vat?skip=&take=&select=&where=&groupBy=&orderBy=`

Returns the items that meet the criteria.

`/vat?skip=24&take=25`

### POST `/vat`

Adds several vats to persist. The service returns the added vats.

```json
[{
    "id": null,
    "name": "Vat for 20%",
    "percentage": 0.2
}]
```

### PUT `/vat`

Updates several vats. The service returns the updated vats.

```json
[{
    "id": "9798ff07-f9d5-462d-acb1-bd58b553ff2e",
    "name": "Vat for 20%",
    "percentage": 0.2
}]
```

### DELETE `/vat`

Removes several vats. The service returns the removed vats.

```json
["9798ff07-f9d5-462d-acb1-bd58b553ff2e"]
```