using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Repositories;

public interface ISensorDataRepository<T>
{
    Task<T> GetLatestValue();

    Task<List<T>> FindSensorDataAsync(FilterDefinition<T> filter);

    Task<double> GetAverageSensorDataValueAsync();

    Task InsertSensorDataAsync(T data);
}
