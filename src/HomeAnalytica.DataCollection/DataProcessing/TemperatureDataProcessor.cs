using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class TemperatureDataProcessor : SensorDataProcessor<TemperatureData>, ITemperatureDataProcessor
{
    public TemperatureDataProcessor(ITemperatureDataRepository repository) : base(repository)
    {
    }

    public override async Task<GetSensorDataResponse> GetSensorData(long deviceId)
    {
        var filter = Builders<TemperatureData>.Filter.Eq(data => data.DeviceId, deviceId);
        var res = await _repository.FindSensorDataAsync(filter);

        var response = new GetSensorDataResponse
        {
            Records = { res.Select(r => new SensorDataRecord
            {
                DeviceId = r.DeviceId,
                Timestamp = Timestamp.FromDateTime(r.Timestamp),
                SensorType = SensorType.Temperature,
                Value = r.Temperature
            })}
        };

        return response;
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

