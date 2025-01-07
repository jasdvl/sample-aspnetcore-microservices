namespace HomeAnalytica.Web.Components.Models;

/// <summary>
/// Represents a model for sensor data used in the UI for data binding and validation.
/// It contains properties such as device id, name, measured quantity id, timestamp, and value.
/// Once data are submitted, the values are typically copied to a Data Transfer Object (DTO) for further processing.
/// </summary>
public class SensorDataModel
{
    /// <summary>
    /// Gets or sets the unique sensor device id.
    /// </summary>
    public long? DeviceId { get; set; }

    /// <summary>
    /// Gets or sets the measured quantity id.
    /// </summary>
    public int MeasuredQuantityId { get; set; }

    /// <summary>
    /// Gets or sets the time of measurement.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the temperature measured by the sensor in degrees Celsius.
    /// </summary>
    public double? Value { get; set; }
}
