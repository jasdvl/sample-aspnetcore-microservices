using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Repositories;

public class TemperatureDataRepository : ITemperatureDataRepository
{
    private readonly IMongoCollection<TemperatureData> _temperatureDataCollection;

    public TemperatureDataRepository(SensorDataDbContext dbContext)
    {
        _temperatureDataCollection = dbContext.TemperatureDataCollection;
    }

    public async Task InsertSensorDataAsync(TemperatureData data)
    {
        await _temperatureDataCollection.InsertOneAsync(data);
    }

    public async Task<TemperatureData> GetLatestValue()
    {
        var sort = Builders<TemperatureData>.Sort.Descending(data => data.Timestamp);

        var result = await _temperatureDataCollection.Find(FilterDefinition<TemperatureData>.Empty)
                                      .Sort(sort)
                                      .Limit(1)
                                      .FirstOrDefaultAsync();
        return result;
    }

    public async Task<List<TemperatureData>> FindSensorDataAsync(FilterDefinition<TemperatureData> filter)
    {
        return await _temperatureDataCollection.Find(filter).ToListAsync();
    }

    public async Task<double> GetAverageTemperatureAsync()
    {
        var averageTemperature = await _temperatureDataCollection
            .Aggregate()
            .Group(sensor => 1, g => new { AverageTemp = g.Average(s => s.Temperature) })
            .FirstOrDefaultAsync();

        return averageTemperature?.AverageTemp ?? 0.0;
    }
}
