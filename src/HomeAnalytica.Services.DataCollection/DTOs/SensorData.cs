namespace HomeAnalytica.DataCollection.DTOs
{
    /// <summary>
    /// Represents the sensor data collected from IoT devices.
    /// </summary>
    public class SensorData
    {
        /// <summary>
        /// Gets or sets the temperature measured by the sensor in degrees Celsius.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Gets or sets the humidity level measured by the sensor in percentage.
        /// </summary>
        public double Humidity { get; set; }

        /// <summary>
        /// Gets or sets the energy consumption measured by the sensor in kilowatt-hours (kWh).
        /// </summary>
        public double EnergyConsumption { get; set; }
    }
}
