namespace HomeAnalytica.Common.DTOs;

/// <summary>
/// Represents a Data Transfer Object (DTO) for a measured quantity, such as "Temperature", "Energy Consumption".
/// This is used for transferring data across layers, distinct from the entity representation in the database.
/// </summary>
public class MeasuredQuantityDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the measured quantity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the measured quantity (e.g., "Temperature", "Energy Consumption").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets a description of the measured quantity.
    /// </summary>
    public string? Description { get; set; }
}

