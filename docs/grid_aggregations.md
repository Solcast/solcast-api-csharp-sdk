# Grid Aggregations

For full details, see https://docs.solcast.com.au/

| Endpoint | Purpose |
| --- | --- |
| [GetForecastAggregation](#getforecastaggregation) | Get forecast aggregation data for up to 14 days of data at a time for a requested collection or aggregation. |
| [GetLiveAggregation](#getliveaggregation) | Get live aggregation data for up to 7 days of data at a time for a requested collection or aggregation. |

## GetForecastAggregation

Get forecast aggregation data for up to 14 days of data at a time for a requested collection or aggregation.

`GET /data/forecast/aggregations`

```csharp
var response = await client.Data.Forecast.Aggregations.GetAsync(request =>
{
    request.QueryParameters.CollectionId = collectionId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| OutputParameters | string[] | No | The output parameters to include in the response. Allowed values: percentage, pv_estimate, percentage10, percentage90, pv_estimate10, pv_estimate90. |
| CollectionId | string | Yes | Unique identifier for your collection. |
| AggregationId | string | No | Unique identifier that belongs to the requested collection. |
| Hours | integer | No | The number of hours to return in the response. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| Format | Format | No | Response format |

**Returns** `ForecastAggregationResponse`

| Field | Description |
| --- | --- |
| forecasts | Forecast aggregated measurement records. |

## GetLiveAggregation

Get live aggregation data for up to 7 days of data at a time for a requested collection or aggregation.

`GET /data/live/aggregations`

```csharp
var response = await client.Data.Live.Aggregations.GetAsync(request =>
{
    request.QueryParameters.CollectionId = collectionId;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| OutputParameters | string[] | No | The output parameters to include in the response. Allowed values: percentage, pv_estimate. |
| CollectionId | string | Yes | Unique identifier for your collection. |
| AggregationId | string | No | Unique identifier that belongs to the requested collection. |
| Hours | integer | No | The number of hours to return in the response. |
| Period | Period | No | Length of the averaging period in ISO 8601 duration format. Default is PT30M. |
| Format | Format | No | Response format |

**Returns** `LiveAggregationResponse`

| Field | Description |
| --- | --- |
| estimated_actuals | Live aggregated measurement records. |
