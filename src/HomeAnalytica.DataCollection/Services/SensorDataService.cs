using Grpc.Core;
using HomeAnalytica.DataCollection.DataProcessing;
using HomeAnalytica.DataCollection.Factories;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.Services;

public class SensorDataService : SensorDataSender.SensorDataSenderBase
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

    public override async Task<SensorDataResponse> SubmitSensorData(SensorDataRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Received sensor data: Value = {request.Value}");

        await HandleSensorDataAsync(request);

        return new SensorDataResponse
        {
            Message = "Sensor data successfully received and processed"
        };
    }

    private async Task HandleSensorDataAsync(SensorDataRequest request)
    {
        ISensorDataProcessor sensorDataProcessor = _sensorDataProcessorFactory.GetDataProcessor(request.SensorType);

        await sensorDataProcessor.HandleSensorDataAsync(request);
    }
}
