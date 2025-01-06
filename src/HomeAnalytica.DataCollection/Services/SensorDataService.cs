using Grpc.Core;
using HomeAnalytica.Common.Const;
using HomeAnalytica.DataCollection.DataProcessing;
using HomeAnalytica.DataCollection.Factories;
using HomeAnalytica.Grpc.Contracts.DataCollection;

namespace HomeAnalytica.DataCollection.Services;

public class SensorDataService : DeviceDataService.DeviceDataServiceBase
{
    private readonly ILogger<SensorDataService> _logger;

    private readonly ISensorDataHandlerFactory _sensorDataProcessorFactory;

    public SensorDataService(
                            ILogger<SensorDataService> logger,
                            ISensorDataHandlerFactory sensorDataHandlerFactory)
    {
        _logger = logger;
        _sensorDataProcessorFactory = sensorDataHandlerFactory;
    }

    public override async Task<SubmitSensorDataResponse> SubmitData(SubmitSensorDataRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Received sensor data: Value = {request.Value}");

        await HandleSensorDataAsync(request);

        return new SubmitSensorDataResponse
        {
            StatusMessage = "Sensor data successfully received and processed"
        };
    }

    public override async Task<GetSensorDataResponse> GetDataByQuantity(GetSensorDataRequest request, ServerCallContext context)
    {
        ISensorDataProcessor sensorDataProcessor = _sensorDataProcessorFactory.GetDataProcessor((MeasuredQuantity) request.MeasuredQuantity);

        var response = await sensorDataProcessor.GetSensorData(request.DeviceId);

        return response;
    }

    private async Task HandleSensorDataAsync(SubmitSensorDataRequest request)
    {
        ISensorDataProcessor sensorDataProcessor = _sensorDataProcessorFactory.GetDataProcessor((MeasuredQuantity) request.MeasuredQuantity);

        await sensorDataProcessor.HandleSensorDataAsync(request);
    }
}
