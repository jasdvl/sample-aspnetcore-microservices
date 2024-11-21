using HomeAnalytica.DataCollection.Data.Entities;
using MongoDB.Driver;

namespace HomeAnalytica.DataCollection.Data.Context;

public class SensorDataDbContext
{
    private readonly IMongoDatabase _database;

    public SensorDataDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<TemperatureData> TemperatureDataCollection => _database.GetCollection<TemperatureData>("temperature_data");

    public IMongoCollection<HumidityData> HumidityDataCollection => _database.GetCollection<HumidityData>("humidity_data");

    public IMongoCollection<EnergyConsumptionData> EnergyConsumptionDataCollection => _database.GetCollection<EnergyConsumptionData>("energy_consumption_data");
}
