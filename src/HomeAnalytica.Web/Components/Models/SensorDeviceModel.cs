using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace HomeAnalytica.Web.Components.Models;

/// <summary>
/// Represents a model for a sensor device used in the UI for data binding and validation.
/// It contains properties such as serial number, name, measured quantity, and status.
/// This model is used within an <see cref="EditForm"/> to manage user input and perform validation.
/// Once data are submitted, the values are typically copied to a Data Transfer Object (DTO) for further processing.
/// </summary>
public class SensorDeviceModel
{
    /// <summary>
    /// Gets or sets the unique serial number of the sensor device.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string SerialNo { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name or label of the sensor for easier identification.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the measured quantity associated with the sensor (e.g., Temperature, Humidity).
    /// </summary>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid measured quantity.")]
    public int MeasuredQuantityId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the physical unit associated with the measured quantity (e.g., °C, %, kWh).
    /// </summary>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid measured quantity.")]
    public int PhysicalUnitId { get; set; }

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
    [Required]
    public string Status { get; set; } = "Active";

    /// <summary>
    /// Gets or sets the date when the sensor was last maintained or calibrated.
    /// </summary>
    public DateTime? LastMaintenance { get; set; }
}
