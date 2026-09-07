# Schedule

For full details, see https://docs.solcast.com.au/

| Endpoint | Purpose |
| --- | --- |
| [DeleteResourceSchedule](#deleteresourceschedule) | Remove stored schedule data for the given time window and schedule type(s). Intervals extending beyond the window are trimmed; intervals entirely within are dropped. |
| [GetResourceSchedule](#getresourceschedule) | Retrieve the current availability and curtailment schedule for a premium resource. Use issue_time to query historical schedule state. |
| [PatchResourceSchedule](#patchresourceschedule) | Merge new availability and/or curtailment schedule intervals into the existing stored schedule for a premium resource. Overlapping intervals are replaced (existing intervals are split as needed). The request is all-or-nothing: a validation error in any entry rejects the entire request. |

## DeleteResourceSchedule

Remove stored schedule data for the given time window and schedule type(s). Intervals extending beyond the window are trimmed; intervals entirely within are dropped.

`DELETE /manage/schedules`

```csharp
var response = await client.Manage.Schedules.DeleteAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
    request.QueryParameters.PeriodStart = periodStart;
    request.QueryParameters.PeriodEnd = periodEnd;
    request.QueryParameters.ScheduleType = scheduleType;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The resource id. |
| PeriodStart | string | Yes | Start of the delete window (ISO 8601). Must be on a 5-minute boundary. |
| PeriodEnd | string | Yes | End of the delete window (ISO 8601). Must be on a 5-minute boundary. |
| ScheduleType | string | Yes | Comma-separated list of schedule types to delete: availability, curtailment |

**Returns** `DeleteResourceScheduleResponse`

| Field | Description |
| --- | --- |
| response_status | Error details returned when the request fails; omitted on success. |

## GetResourceSchedule

Retrieve the current availability and curtailment schedule for a premium resource. Use issue_time to query historical schedule state.

`GET /manage/schedules`

```csharp
var response = await client.Manage.Schedules.GetAsync(request =>
{
    request.QueryParameters.ResourceId = resourceId;
    request.QueryParameters.PeriodStart = periodStart;
    request.QueryParameters.PeriodEnd = periodEnd;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| ResourceId | string | Yes | The resource id. |
| PeriodStart | string | Yes | Start of the query window (ISO 8601). Must be on a 5-minute boundary. |
| PeriodEnd | string | Yes | End of the query window (ISO 8601). Must be on a 5-minute boundary. |
| IssueTime | string | No | Return the schedule as it existed at this point in time (ISO 8601). When omitted, the latest schedule is returned. |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `ResourceScheduleResponse`

| Field | Description |
| --- | --- |
| availability_schedule | Availability schedule intervals for the resource. |
| curtailment_schedule | Curtailment schedule intervals for the resource. |
| response_status | Error details returned when the request fails; omitted on success. |

## PatchResourceSchedule

Merge new availability and/or curtailment schedule intervals into the existing stored schedule for a premium resource. Overlapping intervals are replaced (existing intervals are split as needed). The request is all-or-nothing: a validation error in any entry rejects the entire request.

`PATCH /manage/schedules`

```csharp
var response = await client.Manage.Schedules.PatchAsync();
```

**Returns** `PatchResourceScheduleResponse`

| Field | Description |
| --- | --- |
| response_status | Error details returned when the request fails; omitted on success. |
