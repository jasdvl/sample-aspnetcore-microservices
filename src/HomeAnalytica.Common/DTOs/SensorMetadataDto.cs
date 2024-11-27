using HomeAnalytica.Common.Const;

namespace HomeAnalytica.Common.DTOs;

public class SensorMetadataDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the sensor.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the unique device id of the sensor.
    /// </summary>
    public required string DeviceId { get; set; }

    /// <summary>
    /// Gets or sets the location of the sensor, such as a room name, building, or GPS coordinates.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets additional information or a description of the sensor.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the name or label of the sensor for easier identification.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the sensor (e.g., "Temperature", "Humidity", "EnergyConsumption").
    /// </summary>
    public SensorType Type { get; set; }

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
