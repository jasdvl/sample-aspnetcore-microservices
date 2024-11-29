using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.DataProcessing;

public class EnergyConsumptionDataProcessor : SensorDataProcessor<EnergyConsumptionData>, IEnergyConsumptionDataProcessor
{
    public EnergyConsumptionDataProcessor(IEnergyConsumptionDataRepository repository) : base(repository)
    {
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

