namespace HomeAnalytica.DataCollection.Services;

using global::HomeAnalytica.DataCollection.DTOs;
using HomeAnalytica.Grpc.Contracts.Protos;
using Microsoft.Extensions.Logging;

public class SensorDataService : ISensorDataService
{
    private readonly ILogger<SensorDataService> _logger;

    private readonly SensorDataSender.SensorDataSenderClient _sensorDataSenderClient;

    public SensorDataService(ILogger<SensorDataService> logger, SensorDataSender.SensorDataSenderClient sender)
    {
        _logger = logger;
        _sensorDataSenderClient = sender;
    }

    public async Task ProcessSensorDataAsync(SensorData data)
    {
        // Logik zur Verarbeitung der empfangenen Daten
        _logger.LogInformation($"Processing data: Temp={data.Temperature}, Humidity={data.Humidity}, Energy={data.EnergyConsumption}");

        await _sensorDataSenderClient.SubmitSensorDataAsync(new SensorDataRequest() { EnergyConsumption = 9 });

        await Task.CompletedTask; // Simuliere asynchrone Arbeit
    }
}
