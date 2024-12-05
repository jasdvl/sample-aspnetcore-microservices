using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services;

public interface ISensorDeviceService
{
    Task<List<SensorDeviceDto>> GetSensorDevicesAsync();
}

public class SensorDeviceService : ISensorDeviceService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly ILogger<SensorDeviceService> _logger;

    public SensorDeviceService(
                            ILogger<SensorDeviceService> logger,
                            IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<SensorDeviceDto>> GetSensorDevicesAsync()
    {
        var client = _httpClientFactory.CreateClient("YarpClient");

        try
        {
            var devices = await client.GetFromJsonAsync<List<SensorDeviceDto>>("/sensor-devices/get");

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
