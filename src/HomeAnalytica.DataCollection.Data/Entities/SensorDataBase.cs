using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeAnalytica.DataCollection.Data.Entities;

/// <summary>
/// Represents the base class for all sensor data, containing common properties
/// such as the unique identifier, the sensor device ID, and the timestamp of the recorded data.
/// This class serves as a foundation for more specific types of sensor data, like temperature, humidity, or energy consumption.
/// </summary>
public class SensorDataBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the sensor data entry.
    /// This identifier is represented as an ObjectId in MongoDB.
    /// </summary>
    [BsonId]
    [BsonElement("id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the sensor device that recorded the data.
    /// This is used to identify which device generated the measurements.
    /// </summary>
    [BsonElement("sensor_device_id")]
    public string SensorDeviceId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp indicating when the data was recorded.
    /// This provides the exact time of the measurement.
    /// </summary>
    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }
}
