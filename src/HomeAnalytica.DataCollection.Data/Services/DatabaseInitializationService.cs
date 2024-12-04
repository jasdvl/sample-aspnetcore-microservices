using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Services
{
    /// <summary>
    /// Service responsible for ensuring the proper initialization of the database,
    /// including creating necessary indexes for all collections.
    /// </summary>
    public class DatabaseInitializationService
    {
        private readonly SensorDataDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInitializationService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used to access collections for index creation.</param>
        public DatabaseInitializationService(SensorDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Ensures that all required indexes are created for the collections in the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task EnsureIndexesAsync()
        {
            // TemperatureData: "deviceId_index"
            var temperatureIndexKeys = Builders<TemperatureData>.IndexKeys.Ascending(data => data.DeviceId);
            var temperatureIndexModel = new CreateIndexModel<TemperatureData>(
                temperatureIndexKeys,
                new CreateIndexOptions { Name = "deviceId_index" }
            );

            // TemperatureData: "deviceId_timestamp_index"
            var combinedTemperatureIndexKeys = Builders<TemperatureData>.IndexKeys
                .Ascending(data => data.DeviceId)
                .Descending(data => data.Timestamp);
            var combinedTemperatureIndexModel = new CreateIndexModel<TemperatureData>(
                combinedTemperatureIndexKeys,
                new CreateIndexOptions { Name = "deviceId_timestamp_index" }
            );

            await _dbContext.TemperatureDataCollection.Indexes.CreateOneAsync(temperatureIndexModel);
            await _dbContext.TemperatureDataCollection.Indexes.CreateOneAsync(combinedTemperatureIndexModel);

            // HumidityData: "deviceId_index"
            var humidityIndexKeys = Builders<HumidityData>.IndexKeys.Ascending(data => data.DeviceId);
            var humidityIndexModel = new CreateIndexModel<HumidityData>(
                humidityIndexKeys,
                new CreateIndexOptions { Name = "deviceId_index" }
            );

            // HumidityData: "deviceId_timestamp_index"
            var combinedHumidityIndexKeys = Builders<HumidityData>.IndexKeys
                .Ascending(data => data.DeviceId)
                .Descending(data => data.Timestamp);
            var combinedHumidityIndexModel = new CreateIndexModel<HumidityData>(
                combinedHumidityIndexKeys,
                new CreateIndexOptions { Name = "deviceId_timestamp_index" }
            );

            await _dbContext.HumidityDataCollection.Indexes.CreateOneAsync(humidityIndexModel);
            await _dbContext.HumidityDataCollection.Indexes.CreateOneAsync(combinedHumidityIndexModel);

            // EnergyConsumptionData: "deviceId_index"
            var energyConsumptionIndexKeys = Builders<EnergyConsumptionData>.IndexKeys.Ascending(data => data.DeviceId);
            var energyConsumptionIndexModel = new CreateIndexModel<EnergyConsumptionData>(
                energyConsumptionIndexKeys,
                new CreateIndexOptions { Name = "deviceId_index" }
            );

            // EnergyConsumptionData: "deviceId_timestamp_index"
            var combinedEnergyConsumptionIndexKeys = Builders<EnergyConsumptionData>.IndexKeys
                .Ascending(data => data.DeviceId)
                .Descending(data => data.Timestamp);
            var combinedEnergyConsumptionIndexModel = new CreateIndexModel<EnergyConsumptionData>(
                combinedEnergyConsumptionIndexKeys,
                new CreateIndexOptions { Name = "deviceId_timestamp_index" }
            );

            await _dbContext.EnergyConsumptionDataCollection.Indexes.CreateOneAsync(energyConsumptionIndexModel);
            await _dbContext.EnergyConsumptionDataCollection.Indexes.CreateOneAsync(combinedEnergyConsumptionIndexModel);
        }
    }
}
