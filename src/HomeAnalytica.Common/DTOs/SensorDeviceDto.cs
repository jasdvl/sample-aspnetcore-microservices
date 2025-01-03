using HomeAnalytica.Common.Const;

namespace HomeAnalytica.Common.DTOs;

public class SensorDeviceDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the sensor.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the unique serial number of the sensor device.
    /// </summary>
    public required string SerialNo { get; set; }

    /// <summary>
    /// Gets or sets the name or label of the sensor for easier identification.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the measured quantity associated with the sensor (e.g., Temperature, Humidity).
    /// </summary>
    public MeasuredQuantity MeasuredQuantity { get; set; }

    /// <summary>
    /// Gets or sets the physical unit associated with the measured quantity (e.g., °C, %, kWh).
    /// </summary>
    public PhysicalUnit PhysUnit { get; set; }

    /// <summary>
    /// Gets or sets additional information or a description of the sensor.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the location of the sensor, such as a room name, building, or GPS coordinates.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the date when the sensor was installed.
    /// </summary>
    public DateTime? InstallationDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the sensor, like "Active", "Inactive", or "Maintenance".
    /// </summary>
    public string? Status { get; set; } = "Active";

    /// <summary>
    /// Gets or sets the date when the sensor was last maintained or calibrated.
    /// </summary>
    public DateTime? LastMaintenance { get; set; }
}
