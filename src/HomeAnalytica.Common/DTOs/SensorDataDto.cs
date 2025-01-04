using HomeAnalytica.Common.Const;

namespace HomeAnalytica.Common.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) that holds sensor data collected from IoT devices.
    /// This DTO is used for transferring sensor measurement data between layers or services.
    /// </summary>
    public class SensorDataDto
    {
        /// <summary>
        /// Gets or sets the unique sensor device id.
        /// </summary>
        public long DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the measured quantity.
        /// </summary>
        public MeasuredQuantity MeasuredQuantity { get; set; }

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
