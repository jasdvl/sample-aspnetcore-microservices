namespace HomeAnalytica.DataRegistry.Services;

using HomeAnalytica.Common.DTOs;

/// <summary>
/// Defines the contract for a service that handles operations related to sensor devices.
/// </summary>
public interface ISensorDeviceService
{
    /// <summary>
    /// Asynchronously adds a new sensor device.
    /// </summary>
    /// <param name="metadata">The sensor device data to be added. This includes the metadata like serial number, name, and other relevant information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddSensorDeviceAsync(SensorDeviceDto metadata);

    /// <summary>
    /// Asynchronously retrieves all sensor devices.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="SensorDeviceDto"/> objects.</returns>
    Task<IEnumerable<SensorDeviceDto>> GetAllSensorDevicesAsync();
}
