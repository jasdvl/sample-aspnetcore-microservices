using Grpc.Core;
using HomeAnalytica.Services.Communication.Protos;

namespace HomeAnalytica.Services.Analytics.Grpc;

public class SensorDataService : SensorDataSender.SensorDataSenderBase
{
    private readonly ILogger<SensorDataService> _logger;

    public SensorDataService(ILogger<SensorDataService> logger)
    {
        _logger = logger;
    }

    public override Task<SensorDataResponse> SubmitSensorData(SensorDataRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Received data: Temp={request.Temperature}, Humidity={request.Humidity}, Energy={request.EnergyConsumption}");

        // Prozess der empfangenen Daten (z. B. speichern in einer Datenbank oder weiterverarbeiten)

        return Task.FromResult(new SensorDataResponse
        {
            Message = "Sensor data received successfully"
        });
    }
}
