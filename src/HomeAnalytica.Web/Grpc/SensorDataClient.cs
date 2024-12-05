using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Grpc.Contracts.Protos;

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

    public async Task<GetSensorDataResponse> GetSensorDataByTypeAsync(SensorType sensorType, long deviceId)
    {
        var res = await _client.GetSensorDataByTypeAsync(
                        new GetSensorDataRequest()
                        {
                            SensorType = sensorType,
                            DeviceId = deviceId
                        });
        return res;
    }

    public async Task<SensorDataResponse> SendSensorDataAsync(long deviceId, SensorType sensorType, Timestamp timestamp, double value)
    {
        var request = new SensorDataRequest
        {
            DeviceId = deviceId,
            SensorType = sensorType,
            Timestamp = timestamp,
            Value = value
        };

        var response = await _client.SubmitSensorDataAsync(request);
        _logger.LogDebug($"Response from Analytics Service: {response.Message}");

        return response;
    }
}


