using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Grpc.Contracts.Protos;
using HomeAnalytica.Web.Grpc;

namespace HomeAnalytica.Web.Services;

public class SensorDataService : ISensorDataService
{
    private readonly ILogger<SensorDataService> _logger;

    private readonly SensorDataClient _sensorDataClient;

    public SensorDataService(
                ILogger<SensorDataService> logger,
                SensorDataClient sensorDataClient)
    {
        _logger = logger;
        _sensorDataClient = sensorDataClient;
    }

    public async Task<List<SensorDataDto>> GetSensorDataAsync(HomeAnalytica.Common.Const.SensorType sensorType, long deviceId)
    {
        try
        {
            var data = await _sensorDataClient.GetSensorDataByTypeAsync((SensorType)sensorType, deviceId);

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
                                                                (SensorType)data.SensorType,
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
