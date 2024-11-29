using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Repositories;

/// <summary>
/// Abstract base class for sensor data repository operations.
/// </summary>
/// <typeparam name="T">The type of sensor data, which must inherit from <see cref="SensorDataBase"/>.</typeparam>
public abstract class SensorDataRepository<T> : ISensorDataRepository<T> where T : SensorDataBase
{
    /// <summary>
    /// MongoDB collection containing the sensor data.
    /// </summary>
    protected readonly IMongoCollection<T> _dataCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDataRepository{T}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context used for sensor data.</param>
    /// <param name="dataCollection">The MongoDB collection used to store the sensor data.</param>
    public SensorDataRepository(SensorDataDbContext dbContext, IMongoCollection<T> dataCollection)
    {
        _dataCollection = dataCollection;
    }

    /// <summary>
    /// Inserts a new sensor data entry into the collection asynchronously.
    /// </summary>
    /// <param name="data">The sensor data to be inserted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InsertSensorDataAsync(T data)
    {
        await _dataCollection.InsertOneAsync(data);
    }

    /// <summary>
    /// Retrieves the latest sensor data entry based on the timestamp.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with the latest sensor data entry.</returns>
    public async Task<T> GetLatestValue()
    {
        var sort = Builders<T>.Sort.Descending(data => data.Timestamp);

        var result = await _dataCollection.Find(FilterDefinition<T>.Empty)
                                      .Sort(sort)
                                      .Limit(1)
                                      .FirstOrDefaultAsync();
        return result;
    }

    /// <summary>
    /// Finds sensor data entries matching the specified filter.
    /// </summary>
    /// <param name="filter">The filter definition used to query the sensor data collection.</param>
    /// <returns>A task representing the asynchronous operation, with a list of matching sensor data entries.</returns>
    public async Task<List<T>> FindSensorDataAsync(FilterDefinition<T> filter)
    {
        return await _dataCollection.Find(filter).ToListAsync();
    }

    /// <summary>
    /// Abstract method to calculate the average value of the sensor data.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with the average value of the sensor data.</returns>
    public abstract Task<double> GetAverageSensorDataValueAsync();
}
