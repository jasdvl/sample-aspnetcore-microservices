using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.DataProcessing;

public interface ISensorDataProcessor
{
    Task<GetSensorDataResponse> GetSensorData(long deviceId);

    Task HandleSensorDataAsync(SensorDataRequest request);
}

public abstract class SensorDataProcessor<T> : ISensorDataProcessor where T : SensorDataBase
{
    protected readonly ISensorDataRepository<T> _repository;

    public SensorDataProcessor(ISensorDataRepository<T> repository)
    {
        _repository = repository;
    }

    public abstract Task<GetSensorDataResponse> GetSensorData(long deviceId);

    public abstract Task HandleSensorDataAsync(SensorDataRequest request);
}

