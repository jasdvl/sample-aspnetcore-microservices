using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Grpc.Contracts.DataCollection;

namespace HomeAnalytica.Web.Grpc;

public class SensorDataClient
{
    private readonly ILogger<SensorDataClient> _logger;

    private readonly DeviceDataService.DeviceDataServiceClient _client;

    public SensorDataClient(
                          ILogger<SensorDataClient> logger,
                          DeviceDataService.DeviceDataServiceClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<GetSensorDataResponse> GetDataByQuantityAsync(int measuredQuantityId, long deviceId)
    {
        var res = await _client.GetDataByQuantityAsync(
                        new GetSensorDataRequest()
                        {
                            MeasuredQuantity = measuredQuantityId,
                            DeviceId = deviceId
                        });
        return res;
    }

    public async Task<SubmitSensorDataResponse> SendSensorDataAsync(long deviceId, int measuredQuantityId, Timestamp timestamp, double value)
    {
        var request = new SubmitSensorDataRequest
        {
            DeviceId = deviceId,
            MeasuredQuantity = measuredQuantityId,
            Timestamp = timestamp,
            Value = value
        };

        var response = await _client.SubmitDataAsync(request);
        _logger.LogDebug($"Response from Analytics Service: {response.StatusMessage}");

        return response;
    }
}


