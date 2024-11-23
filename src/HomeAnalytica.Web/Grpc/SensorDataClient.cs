using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.Web.Grpc;

public class SensorDataClient
{
    private readonly SensorDataSender.SensorDataSenderClient _client;

    public SensorDataClient(SensorDataSender.SensorDataSenderClient client)
    {
        _client = client;
    }

    public async Task SendSensorDataAsync(DateTime timestamp, double value)
    {
        var request = new SensorDataRequest
        {
            Timestamp = Timestamp.FromDateTime(timestamp),
            Value = value
        };

        var response = await _client.SubmitSensorDataAsync(request);
        Console.WriteLine($"Response from Analytics Service: {response.Message}");
    }
}


