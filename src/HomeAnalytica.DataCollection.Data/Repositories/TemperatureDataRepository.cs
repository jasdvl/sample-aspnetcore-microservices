using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Repositories;

/// <summary>
/// A repository implementation for managing temperature sensor data in the database.
/// Inherits from <see cref="SensorDataRepository{TemperatureData}"/> and implements
/// <see cref="ITemperatureDataRepository"/>.
/// </summary>
public class TemperatureDataRepository : SensorDataRepository<TemperatureData>, ITemperatureDataRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TemperatureDataRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used for accessing the temperature data collection.</param>
    public TemperatureDataRepository(SensorDataDbContext dbContext)
        : base(dbContext, dbContext.TemperatureDataCollection)
    {
    }

    /// <summary>
    /// Retrieves the average temperature value from the temperature data collection.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The result
    /// contains the average temperature value as a <see cref="double"/>.
    /// </returns>
    public override async Task<double> GetAverageSensorDataValueAsync()
    {
        var averageTemperature = await _dataCollection
            .Aggregate()
            .Group(sensor => 1, g => new { AverageTemp = g.Average(s => s.Temperature) })
            .FirstOrDefaultAsync();

        return averageTemperature?.AverageTemp ?? 0.0;
    }
}

