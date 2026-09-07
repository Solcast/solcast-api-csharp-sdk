# Geographic

For full details, see https://docs.solcast.com.au/

| Endpoint | Purpose |
| --- | --- |
| [GetHorizonAngle](#gethorizonangle) | Returns a circular profile of angles to the horizon for a location at a given latitude, longitude point. The angles are calculated based on the surrounding terrain elevation from a 90m horizontal-resolution digital elevation model. The angle returned is relative to due north, with negative angles eastwards and positive values westward . Varies from -180 to 180. A value of -90 means east, 0 means north, and 90 means west. |

## GetHorizonAngle

Returns a circular profile of angles to the horizon for a location at a given latitude, longitude point. The angles are calculated based on the surrounding terrain elevation from a 90m horizontal-resolution digital elevation model. The angle returned is relative to due north, with negative angles eastwards and positive values westward . Varies from -180 to 180. A value of -90 means east, 0 means north, and 90 means west.

`GET /data/geographic/horizon_angle`

```csharp
var response = await client.Data.Geographic.HorizonAngle.GetAsync(request =>
{
    request.QueryParameters.Latitude = latitude;
    request.QueryParameters.Longitude = longitude;
    request.QueryParameters.AzimuthIntervals = azimuthIntervals;
});
```

**Parameters**

| Name | Type | Required | Description |
| --- | --- | --- | --- |
| Latitude | number | Yes | The latitude of the location you request data for. Must be a decimal number between -90 and 90. |
| Longitude | number | Yes | The longitude of the location you request data for. Must be a decimal number between -180 and 180. |
| AzimuthIntervals | integer | Yes | Number of sections to divide the azimuth circle into, governing how many items are returned in the response. E.g. 10 = [-180°, -144°, -108°, -72°, -36°, 0°, 36°, 72°, 108°, 144°] Maximum 360 |
| Format | Format | No | Response format. Default is HTML if not supplied. |

**Returns** `HorizonAngleResponse`

| Field | Description |
| --- | --- |
| horizon_angles | Horizon angle measurements around the requested location. |
| response_status | Error details returned when the request fails; omitted on success. |
