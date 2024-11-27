using HomeAnalytica.Common.Const;

namespace HomeAnalytica.Common.DTOs
{
    /// <summary>
    /// Represents the sensor data collected from IoT devices.
    /// </summary>
    public class SensorDataDto
    {
        /// <summary>
        /// Gets or sets the unique sensor device id.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the sensor type.
        /// </summary>
        public SensorType SensorType { get; set; }

        /// <summary>
        /// Gets or sets the time of measurement.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the temperature measured by the sensor in degrees Celsius.
        /// </summary>
        public double Value { get; set; }
    }
}
