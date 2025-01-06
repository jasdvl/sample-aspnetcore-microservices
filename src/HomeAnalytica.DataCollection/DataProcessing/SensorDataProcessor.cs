using HomeAnalytica.Common.Const;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;
using HomeAnalytica.Grpc.Contracts.DataCollection;

namespace HomeAnalytica.DataCollection.DataProcessing;

public interface ISensorDataProcessor
{
    MeasuredQuantity MeasuredQuantity { get; }

    Task<GetSensorDataResponse> GetSensorData(long deviceId);

    Task HandleSensorDataAsync(SubmitSensorDataRequest request);
}

public abstract class SensorDataProcessor<T> : ISensorDataProcessor where T : SensorDataBase
{
    protected readonly ISensorDataRepository<T> _repository;

    public SensorDataProcessor(ISensorDataRepository<T> repository)
    {
        _repository = repository;
        MeasuredQuantity = MeasuredQuantity.Unknown;
    }

    public virtual MeasuredQuantity MeasuredQuantity { get; }

    public abstract Task<GetSensorDataResponse> GetSensorData(long deviceId);

    public abstract Task HandleSensorDataAsync(SubmitSensorDataRequest request);
}

