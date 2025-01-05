namespace HomeAnalytica.Common.DTOs;

/// <summary>
/// Represents a Data Transfer Object (DTO) that holds reference data, including lists of measured quantities and physical units.
/// This DTO is used for transferring reference data between layers or services.
/// </summary>
public class ReferenceDataDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReferenceDataDto"/> class.
    /// </summary>
    public ReferenceDataDto()
    {
        MeasuredQuantityDtos = new List<MeasuredQuantityDto>();
        PhysicalUnitDtos = new List<PhysicalUnitDto>();
    }

    /// <summary>
    /// Gets or sets the list of measured quantity DTOs (e.g., "Temperature", "Energy Consumption").
    /// </summary>
    public List<MeasuredQuantityDto> MeasuredQuantityDtos { get; set; }

    /// <summary>
    /// Gets or sets the list of physical unit DTOs (e.g., "Degrees Celsius", "Percent").
    /// </summary>
    public List<PhysicalUnitDto> PhysicalUnitDtos { get; set; }
}
