using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Grpc.Contracts.DataCollection;

namespace HomeAnalytica.Web.Grpc;

public class SensorDataClient
{
    private readonly ILogger<SensorDataClient> _logger;

    private readonly SensorDataSender.SensorDataSenderClient _client;

    public SensorDataClient(
                          ILogger<SensorDataClient> logger,
                          SensorDataSender.SensorDataSenderClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<GetSensorDataResponse> GetSensorDataByTypeAsync(MeasuredQuantity measuredQuantity, long deviceId)
    {
        var res = await _client.GetSensorDataByTypeAsync(
                        new GetSensorDataRequest()
                        {
                            MeasuredQuantity = measuredQuantity,
                            DeviceId = deviceId
                        });
        return res;
    }

    public async Task<SensorDataResponse> SendSensorDataAsync(long deviceId, MeasuredQuantity measuredQuantity, Timestamp timestamp, double value)
    {
        var request = new SensorDataRequest
        {
            DeviceId = deviceId,
            MeasuredQuantity = measuredQuantity,
            Timestamp = timestamp,
            Value = value
        };

        var response = await _client.SubmitSensorDataAsync(request);
        _logger.LogDebug($"Response from Analytics Service: {response.Message}");

        return response;
    }
}


