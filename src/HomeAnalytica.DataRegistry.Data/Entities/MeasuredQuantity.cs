namespace HomeAnalytica.DataRegistry.Data.Entities;

/// <summary>
/// Represents a measurable physical quantity.
/// </summary>
public class MeasuredQuantity
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

