using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.DataCollection;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class EnergyConsumptionDataProcessor : SensorDataProcessor<EnergyConsumptionData>, IEnergyConsumptionDataProcessor
{
    public EnergyConsumptionDataProcessor(IEnergyConsumptionDataRepository repository) : base(repository)
    {
    }

    public override async Task<GetSensorDataResponse> GetSensorData(long deviceId)
    {
        var filter = Builders<EnergyConsumptionData>.Filter.Eq(data => data.DeviceId, deviceId);
        var res = await _repository.FindSensorDataAsync(filter);

        var response = new GetSensorDataResponse
        {
            Records = { res.Select(r => new SensorDataRecord
            {
                DeviceId = r.DeviceId,
                Timestamp = Timestamp.FromDateTime(r.Timestamp),
                MeasuredQuantity = MeasuredQuantity.EnergyConsumption,
                Value = r.TotalEnergyConsumption
            })}
        };

        return response;
    }

    public override async Task HandleSensorDataAsync(SensorDataRequest request)
    {
        var sensorData = new EnergyConsumptionData
        {
            DeviceId = request.DeviceId,
            Timestamp = request.Timestamp.ToDateTime(),
            TotalEnergyConsumption = request.Value
        };

        await _repository.InsertSensorDataAsync(sensorData);
    }
}

