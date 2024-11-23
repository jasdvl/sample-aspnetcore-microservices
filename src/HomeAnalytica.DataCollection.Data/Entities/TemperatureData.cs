using MongoDB.Bson.Serialization.Attributes;

namespace HomeAnalytica.DataCollection.Data.Entities;

/// <summary>
/// Represents the data recorded by a temperature sensor,
/// inheriting common properties from SensorDataBase.
/// </summary>
public class TemperatureData : SensorDataBase
{
    /// <summary>
    /// Gets or sets the temperature value recorded by the sensor, measured in Celsius degrees (°C).
    /// </summary>
    [BsonElement("temperature")]
    public double Temperature { get; set; }
}
