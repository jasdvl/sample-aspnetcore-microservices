using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeAnalytica.DataCollection.Data.Entities;

/// <summary>
/// Represents humidity data recorded by a sensor, stored in the MongoDB database.
/// This class extends the base properties from <see cref="SensorDataBase"/> 
/// to include a specific measurement for humidity.
/// </summary>
public class HumidityData : SensorDataBase
{
    /// <summary>
    /// Gets or sets the relative humidity level recorded by the sensor, expressed as a percentage.
    /// </summary>
    [BsonElement("humidity")]
    public double Humidity { get; set; }
}
