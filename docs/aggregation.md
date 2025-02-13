# Aggregation Data API

Get live or forecast aggregation data for up to 7 days of data at a time for a requested collection or aggregation.

---


The module AggregationClient has the following available methods:

| Endpoint                  | Purpose                                              | API Docs                                                                                                               |
|---------------------------|------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------|
| [GetLiveAggregations](#getliveaggregations) | Get live aggregation data for up to 7 days of data at a time for a requested collection or aggregation. | [details](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a){.md-button} |
| [GetForecastAggregations](#getforecastaggregations) | Get forecast aggregation data for up to 7 days of data at a time for a requested collection or aggregation. | [details](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb){.md-button} |

---

### GetLiveAggregations
**Parameters:**
[outputParameters](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a "(List<string>): The output parameters to include in the response. (Optional)"), [collectionId](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a "(string): Unique identifier for your collection. (Optional)"), [aggregationId](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a "(string): Unique identifier that belongs to the requested collection. (Optional)"), [hours](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a "(int?): The number of hours to return in the response. (Optional)"), [period](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [format](https://docs.solcast.com.au/#3b09628d-0f9d-4a01-aa53-9af460d6c66a "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var aggregationClient = new AggregationClient();
var response = await aggregationClient.GetLiveAggregations(
    outputParameters: ["percentage", "pv_estimate"],
    collectionId: "aust_state_total",
    aggregationId: "vic",
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| PvEstimate | Percentage | PeriodEnd | Period |
| --- | --- | --- | --- |
| 2175.2377 | 41.1 | 2025-02-13T23:30:00+00:00 | PT30M |
| 1869.4536 | 35.4 | 2025-02-13T23:00:00+00:00 | PT30M |
| ... | ... | ... | ... |
| 1332.7474 | 25.3 | 2025-02-07T00:30:00+00:00 | PT30M |
| 1234.823 | 23.4 | 2025-02-07T00:00:00+00:00 | PT30M |

---

### GetForecastAggregations
**Parameters:**
[outputParameters](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb "(List<string>): The output parameters to include in the response. (Optional)"), [collectionId](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb "(string): Unique identifier for your collection. (Optional)"), [aggregationId](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb "(string): Unique identifier that belongs to the requested collection. (Optional)"), [hours](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb "(int?): The number of hours to return in the response. (Optional)"), [period](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb "(string): Length of the averaging period in ISO 8601 format. (Optional)"), [format](https://docs.solcast.com.au/#feeb0565-ac06-473a-8cd1-b1493c5bcabb "(string): Response format (Optional)")

**Example Usage:**
```csharp
using Solcast.Clients;

var aggregationClient = new AggregationClient();
var response = await aggregationClient.GetForecastAggregations(
    outputParameters: ["percentage", "pv_estimate"],
    collectionId: "aust_state_total",
    aggregationId: "vic",
    format: "csv"
);
Console.WriteLine(response.RawResponse);

```
**Sample Output:**

| Percentage | PvEstimate | PeriodEnd | Period |
| --- | --- | --- | --- |
| 45.5 | 2405.8780 | 2025-02-14T00:00:00+00:00 | PT30M |
| 47.9 | 2532.9318 | 2025-02-14T00:30:00+00:00 | PT30M |
| ... | ... | ... | ... |
| 36.6 | 1938.2761 | 2025-02-20T23:00:00+00:00 | PT30M |
| 43.3 | 2291.6608 | 2025-02-20T23:30:00+00:00 | PT30M |

---
