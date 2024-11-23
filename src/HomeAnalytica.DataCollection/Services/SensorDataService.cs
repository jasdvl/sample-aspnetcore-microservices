using Grpc.Core;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.Services;

public class SensorDataService : SensorDataSender.SensorDataSenderBase
{
    private readonly ILogger<SensorDataService> _logger;

    private readonly ITemperatureDataRepository _temperatureDataRepository;

    public SensorDataService(ILogger<SensorDataService> logger, ITemperatureDataRepository temperatureDataRepository)
    {
        _logger = logger;
        _temperatureDataRepository = temperatureDataRepository;
    }

    public override async Task<SensorDataResponse> SubmitSensorData(SensorDataRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Received data: Value = {request.Value}");

        await Insert(request);

        return new SensorDataResponse
        {
            Message = "Data successfully received and processed"
        };
    }

    private async Task Insert(SensorDataRequest request)
    {
        throw new NotImplementedException();

        //switch (request.SensorType)
        //{

        //}

        //var sensorData = new TemperatureData
        //{
        //    Timestamp = request.Timestamp.ToDateTime(),
        //    Temperature = request.Value
        //};

        // await _temperatureDataRepository.InsertSensorDataAsync(sensorData);
    }
}
