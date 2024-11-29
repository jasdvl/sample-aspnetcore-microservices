using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class HumidityDataProcessor : SensorDataProcessor<HumidityData>, IHumidityDataProcessor
{
    public HumidityDataProcessor(IHumidityDataRepository repository) : base(repository)
    {
    }

    public override async Task HandleSensorDataAsync(SensorDataRequest request)
    {
        var sensorData = new HumidityData
        {
            DeviceId = request.DeviceId,
            Timestamp = request.Timestamp.ToDateTime(),
            Humidity = request.Value
        };

        await _repository.InsertSensorDataAsync(sensorData);
    }
}

