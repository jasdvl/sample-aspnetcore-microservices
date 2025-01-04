using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataRegistry.Data.Infrastructure;

namespace HomeAnalytica.DataRegistry.Services;

/// <summary>
/// Service class responsible for retrieving reference data, including measured quantities and physical units.
/// </summary>
public class ReferenceDataService : IReferenceDataService
{
    private readonly ILogger<ReferenceDataService> _logger;

    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReferenceDataService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for logging service-related information.</param>
    /// <param name="unitOfWork">The unit of work instance to interact with repositories.</param>
    public ReferenceDataService(
                                ILogger<ReferenceDataService> logger,
                                IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Retrieves the reference data, including lists of measured quantities and physical units.
    /// </summary>
    /// <returns>A <see cref="ReferenceDataDto"/> containing the measured quantities and physical units.</returns>
    public async Task<ReferenceDataDto> GetReferenceDataAsync()
    {
        var measuredQuantities = await _unitOfWork.MeasuredQuantityRepository.GetAsync();
        var physicalUnits = await _unitOfWork.PhysicalUnitRepository.GetAsync();

        var measuredQuantityDtos = measuredQuantities.Select(entity => new MeasuredQuantityDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        });

        var physicalUnitDtos = physicalUnits.Select(entity => new PhysicalUnitDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Symbol = entity.Symbol,
            Description = entity.Description
        });

        var referenceData = new ReferenceDataDto { MeasuredQuantityDtos = measuredQuantityDtos.ToList(), PhysicalUnitDtos = physicalUnitDtos.ToList() };

        return referenceData;
    }
}
