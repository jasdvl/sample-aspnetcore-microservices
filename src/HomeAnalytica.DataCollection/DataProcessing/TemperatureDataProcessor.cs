using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class TemperatureDataProcessor : SensorDataProcessor<TemperatureData>, ITemperatureDataProcessor
{
    public TemperatureDataProcessor(ITemperatureDataRepository repository) : base(repository)
    {
    }

    public override async Task HandleSensorDataAsync(SensorDataRequest request)
    {
        var sensorData = new TemperatureData
        {
            DeviceId = request.DeviceId,
            Timestamp = request.Timestamp.ToDateTime(),
            Temperature = request.Value
        };

        await _repository.InsertSensorDataAsync(sensorData);
    }
}

