namespace HomeAnalytica.Common.Const
{
    /// <summary>
    /// Represents the physical units used for sensor measurements.
    /// </summary>
    public enum PhysicalUnit
    {
        /// <summary>
        /// Represents the absence of a physical unit or an undefined unit.
        /// </summary>
        None = 0,

        /// <summary>
        /// Temperature measurement in degrees Celsius.
        /// </summary>
        CelsiusDegrees = 1,

        /// <summary>
        /// Temperature measurement in degrees Fahrenheit.
        /// </summary>
        FahrenheitDegrees = 2,

        /// <summary>
        /// Percentage measurement, typically used for humidity or other relative values.
        /// </summary>
        Percent = 3,

        /// <summary>
        /// Energy consumption measurement in kilowatt-hours.
        /// </summary>
        KiloWattHours = 4
    }
}
