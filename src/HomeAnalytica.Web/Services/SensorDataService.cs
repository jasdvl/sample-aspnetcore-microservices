using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.Web.Services;

public class SensorDataService : ISensorDataService
{
    private readonly ILogger<SensorDataService> _logger;

    private readonly SensorDataSender.SensorDataSenderClient _sensorDataSenderClient;

    public SensorDataService(
                ILogger<SensorDataService> logger,
                SensorDataSender.SensorDataSenderClient sender)
    {
        _logger = logger;
        _sensorDataSenderClient = sender;
    }

    public async Task ProcessSensorDataAsync(SensorDataDto data)
    {
        // Logik zur Verarbeitung der empfangenen Daten
        _logger.LogInformation($"Processing sensor data: Value = {data.Value}");

        try
        {
            var res = await _sensorDataSenderClient.SubmitSensorDataAsync(
                                                        new SensorDataRequest()
                                                        {
                                                            SensorType = (int)data.SensorType,
                                                            Timestamp = Timestamp.FromDateTime(data.Timestamp),
                                                            Value = data.Value
                                                        });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing sensor data.");

            throw;
        }
    }
}
