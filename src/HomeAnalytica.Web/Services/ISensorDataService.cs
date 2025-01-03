using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services;

/// <summary>
/// Defines the contract for the service that handles sensor data operations, including retrieving and processing sensor data.
/// </summary>
public interface ISensorDataService
{
    /// <summary>
    /// Asynchronously retrieves sensor data based on the specified sensor type and device ID.
    /// </summary>
    /// <param name="measuredQuantity">The type of the sensor to retrieve data for.</param>
    /// <param name="deviceId">The ID of the device to retrieve data from.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of <see cref="SensorDataDto"/>.</returns>
    /// <exception cref="Exception">Thrown when an error occurs during the retrieval of sensor data.</exception>
    Task<List<SensorDataDto>> GetSensorDataAsync(MeasuredQuantity measuredQuantity, long deviceId);

    /// <summary>
    /// Asynchronously processes and sends sensor data to the sensor data service.
    /// </summary>
    /// <param name="data">The sensor data to be processed and sent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when an error occurs while processing sensor data.</exception>
    Task ProcessSensorDataAsync(SensorDataDto data);
}
