using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
        catch (RpcException rpcEx)
        {
            _logger.LogError(rpcEx, "gRPC error while requesting sensor data. Status: {StatusCode}, DeviceId: {DeviceId}, MeasuredQuantity: {MeasuredQuantity}",
                rpcEx.StatusCode, deviceId, measuredQuantity);
            throw new WebServiceException("Unable to get sensor data due to a server issue.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while requesting sensor data. DeviceId: {DeviceId}, MeasuredQuantity: {MeasuredQuantity}",
                deviceId, measuredQuantity);
            throw new WebServiceException("An unexpected error occurred.");
        }
    }

    public async Task ProcessSensorDataAsync(SensorDataDto data)
    {
        _logger.LogInformation("Processing sensor data: DeviceId = {DeviceId}, MeasuredQuantity = {MeasuredQuantity}, Value = {Value}",
            data.DeviceId, data.MeasuredQuantity, data.Value);

        try
        {
            var res = await _sensorDataClient.SendSensorDataAsync(
                data.DeviceId,
                (MeasuredQuantity) data.MeasuredQuantity,
                Timestamp.FromDateTime(data.Timestamp),
                data.Value
            );

            _logger.LogDebug("gRPC response: {Response}", res);
        }
        catch (RpcException rpcEx)
        {
            _logger.LogError(rpcEx, "gRPC error while processing sensor data. Status: {StatusCode}, DeviceId: {DeviceId}, MeasuredQuantity: {MeasuredQuantity}, Value: {Value}",
                rpcEx.StatusCode, data.DeviceId, data.MeasuredQuantity, data.Value);
            throw new WebServiceException("Unable to process sensor data due to a server issue.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while processing sensor data. DeviceId: {DeviceId}, MeasuredQuantity: {MeasuredQuantity}, Value: {Value}",
                data.DeviceId, data.MeasuredQuantity, data.Value);
            throw new WebServiceException("An unexpected error occurred.");
        }
    }
}
