namespace HomeAnalytica.Common.DTOs;

/// <summary>
/// Represents a Data Transfer Object (DTO) for a physical unit, such as temperature (°C), humidity (%), or energy consumption (kWh).
/// This is used for transferring data across layers, distinct from the entity representation in the database.
/// </summary>
public class PhysicalUnitDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the physical unit.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the physical unit (e.g., "Degrees Celsius", "Percent").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the symbol of the physical unit (e.g., "°C", "%").
    /// </summary>
    public required string Symbol { get; set; }

    /// <summary>
    /// Gets or sets a description of the physical unit.
    /// </summary>
    public string? Description { get; set; }
}
