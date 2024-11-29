using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Repositories;

public class HumidityDataRepository : SensorDataRepository<HumidityData>, IHumidityDataRepository
{
    public HumidityDataRepository(SensorDataDbContext dbContext) : base(dbContext, dbContext.HumidityDataCollection)
    {
    }

    public override async Task<double> GetAverageSensorDataValueAsync()
    {
        var averageHumidity = await _dataCollection
            .Aggregate()
            .Group(sensor => 1, g => new { AverageEnergyConsumption = g.Average(s => s.Humidity) })
            .FirstOrDefaultAsync();

        return averageHumidity?.AverageEnergyConsumption ?? 0.0;
    }
}
