namespace HomeAnalytica.DataRegistry.Services;

using HomeAnalytica.Common.DTOs;

/// <summary>
/// Defines the contract for a service that retrieves reference data, including measured quantities and physical units.
/// </summary>
public interface IReferenceDataService
{
    /// <summary>
    /// Asynchronously retrieves reference data, including lists of measured quantities and physical units.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ReferenceDataDto"/> with measured quantities and physical units.</returns>
    Task<ReferenceDataDto> GetReferenceDataAsync();
}
