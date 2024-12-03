using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class HumidityDataProcessor : SensorDataProcessor<HumidityData>, IHumidityDataProcessor
{
    public HumidityDataProcessor(IHumidityDataRepository repository) : base(repository)
    {
    }

    public override async Task<GetSensorDataResponse> GetSensorData()
    {
        var filter = Builders<HumidityData>.Filter.Empty;

        var res = await _repository.FindSensorDataAsync(filter);

        var response = new GetSensorDataResponse
        {
            Records = { res.Select(r => new SensorDataRecord
            {
                DeviceId = r.DeviceId,
                Timestamp = Timestamp.FromDateTime(r.Timestamp.ToUniversalTime()),
                SensorType = SensorType.Humidity,
                Value = r.Humidity
            })}
        };

        return response;
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

