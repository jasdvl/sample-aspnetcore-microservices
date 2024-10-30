using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.Grpc;

public class SensorDataClient
{
    private readonly SensorDataSender.SensorDataSenderClient _client;

    public SensorDataClient(SensorDataSender.SensorDataSenderClient client)
    {
        _client = client;
    }

    public async Task SendSensorDataAsync(double temperature, double humidity, double energyConsumption)
    {
        var request = new SensorDataRequest
        {
            Temperature = temperature,
            Humidity = humidity,
            EnergyConsumption = energyConsumption
        };

        var response = await _client.SubmitSensorDataAsync(request);
        Console.WriteLine($"Response from Analytics Service: {response.Message}");
    }
}


