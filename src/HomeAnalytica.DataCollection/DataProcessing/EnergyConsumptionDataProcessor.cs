using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class EnergyConsumptionDataProcessor : SensorDataProcessor<EnergyConsumptionData>, IEnergyConsumptionDataProcessor
{
    public EnergyConsumptionDataProcessor(IEnergyConsumptionDataRepository repository) : base(repository)
    {
    }

    public override async Task<GetSensorDataResponse> GetSensorData()
    {
        var filter = Builders<EnergyConsumptionData>.Filter.Empty;

        var res = await _repository.FindSensorDataAsync(filter);

        var response = new GetSensorDataResponse
        {
            Records = { res.Select(r => new SensorDataRecord
            {
                DeviceId = r.DeviceId,
                Timestamp = Timestamp.FromDateTime(r.Timestamp.ToUniversalTime()),
                SensorType = SensorType.EnergyConsumption,
                Value = r.EnergyConsumption
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
            EnergyConsumption = request.Value
        };

        await _repository.InsertSensorDataAsync(sensorData);
    }
}

