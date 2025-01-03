using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services
{
    /// <summary>
    /// Defines the contract for services that interact with sensor devices.
    /// </summary>
    public interface ISensorDeviceService
    {
        /// <summary>
        /// Asynchronously retrieves a list of sensor devices.
        /// </summary>
        /// <returns>A <c>Task</c> that represents the asynchronous operation. The task result contains a list of <see cref="SensorDeviceDto"/> objects.</returns>
        Task<List<SensorDeviceDto>> GetSensorDevicesAsync();

        /// <summary>
        /// Asynchronously posts a new sensor device.
        /// </summary>
        /// <param name="sensorDeviceDto">The sensor device to be posted.</param>
        /// <returns>A <c>Task</c> that represents the asynchronous operation.</returns>
        Task PostSensorDeviceAsync(SensorDeviceDto sensorDeviceDto);
    }

    /// <summary>
    /// Provides methods for interacting with sensor devices via HTTP.
    /// </summary>
    public class SensorDeviceService : ISensorDeviceService
    {
        private readonly ILogger<SensorDeviceService> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorDeviceService"/> class.
        /// </summary>
        /// <param name="logger">The logger to use for logging errors.</param>
        /// <param name="httpClient">The HTTP client to make requests.</param>
        public SensorDeviceService(
                                ILogger<SensorDeviceService> logger,
                                HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Asynchronously posts a sensor device to the backend.
        /// </summary>
        /// <param name="sensorDeviceDto">The sensor device to be posted.</param>
        /// <returns>A <c>Task</c> that represents the asynchronous operation.</returns>
        public async Task PostSensorDeviceAsync(SensorDeviceDto sensorDeviceDto)
        {
            if (string.IsNullOrEmpty(sensorDeviceDto.SerialNo) || sensorDeviceDto.MeasuredQuantity == Common.Const.MeasuredQuantity.Unknown)
            {
                throw new ArgumentException("Serial no. and measured quantity are mandatory.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/sensor-devices/post", sensorDeviceDto);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error when sending sensor device: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of sensor devices from the backend.
        /// </summary>
        /// <returns>A <c>Task</c> that represents the asynchronous operation. The task result contains a list of <see cref="SensorDeviceDto"/> objects.</returns>
        public async Task<List<SensorDeviceDto>> GetSensorDevicesAsync()
        {
            try
            {
                var devices = await _httpClient.GetFromJsonAsync<List<SensorDeviceDto>>("/sensor-devices/get");

                return devices ?? new List<SensorDeviceDto>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                return new List<SensorDeviceDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                return new List<SensorDeviceDto>();
            }
        }
    }
}
