using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Repositories;
public interface ITemperatureDataRepository
{
    Task<TemperatureData> GetLatestValue();

    Task<List<TemperatureData>> FindSensorDataAsync(FilterDefinition<TemperatureData> filter);

    Task<double> GetAverageTemperatureAsync();

    Task InsertSensorDataAsync(TemperatureData data);
}
