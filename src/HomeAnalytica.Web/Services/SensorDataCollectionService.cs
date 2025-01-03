using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Grpc.Contracts.DataCollection;
using HomeAnalytica.Web.Grpc;

namespace HomeAnalytica.Web.Services;

/// <summary>
/// Service for handling sensor data operations, including retrieving and processing sensor data.
/// </summary>
public class SensorDataCollectionService : ISensorDataCollectionService
{
    private readonly ILogger<SensorDataCollectionService> _logger;

    private readonly SensorDataClient _sensorDataClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDataCollectionService"/> class.
    /// </summary>
    /// <param name="logger">The logger for logging information and errors.</param>
    /// <param name="sensorDataClient">The client for interacting with the sensor data service.</param>
    public SensorDataCollectionService(
                ILogger<SensorDataCollectionService> logger,
                SensorDataClient sensorDataClient)
    {
        _logger = logger;
        _sensorDataClient = sensorDataClient;
    }

    public async Task<List<SensorDataDto>> GetSensorDataAsync(HomeAnalytica.Common.Const.MeasuredQuantity measuredQuantity, long deviceId)
    {
        try
        {
            var data = await _sensorDataClient.GetDataByQuantityAsync((MeasuredQuantity) measuredQuantity, deviceId);

            var res = data.Records.Select(d => new SensorDataDto
            {
                DeviceId = d.DeviceId,
                Timestamp = d.Timestamp.ToDateTime(),
                Value = d.Value
            }).ToList();

            return res;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while requesting sensor data.");

            throw;
        }
    }

    public async Task ProcessSensorDataAsync(SensorDataDto data)
    {
        _logger.LogInformation($"Processing sensor data: Value = {data.Value}");

        try
        {
            var res = await _sensorDataClient.SendSensorDataAsync(
                                                                data.DeviceId,
                                                                (MeasuredQuantity) data.MeasuredQuantity,
                                                                Timestamp.FromDateTime(data.Timestamp),
                                                                data.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing sensor data.");

            throw;
        }
    }
}
