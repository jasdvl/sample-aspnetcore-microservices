using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services;

public interface ISensorDeviceService
{
    Task<List<SensorMetadataDto>> GetSensorDevicesAsync();
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

    public async Task<List<SensorMetadataDto>> GetSensorDevicesAsync()
    {
        var client = _httpClientFactory.CreateClient("YarpClient");

        try
        {
            var devices = await client.GetFromJsonAsync<List<SensorMetadataDto>>("/sensor-devices/get");

            return devices ?? new List<SensorMetadataDto>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Request error: {ex.Message}");
            return new List<SensorMetadataDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error: {ex.Message}");
            return new List<SensorMetadataDto>();
        }
    }
}
