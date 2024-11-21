using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeAnalytica.DataCollection.Data.Entities;

/// <summary>
/// Represents energy consumption data recorded by a sensor, stored in the MongoDB database.
/// This class extends <see cref="SensorDataBase"/> to include a specific measurement for energy consumption.
/// </summary>
public class EnergyConsumptionData : SensorDataBase
{
    /// <summary>
    /// Gets or sets the energy consumption value recorded by the sensor, measured in kilowatt-hours (kWh).
    /// </summary>
    [BsonElement("energy_consumption")]
    public double EnergyConsumption { get; set; }
}
