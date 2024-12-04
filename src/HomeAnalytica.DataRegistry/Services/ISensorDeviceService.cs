namespace HomeAnalytica.DataRegistry.Services;

using HomeAnalytica.Common.DTOs;

public interface ISensorDeviceService
{
    Task ProcessSensorMetadataAsync(SensorDeviceDto metadata);

    Task<IEnumerable<SensorDeviceDto>> GetAllSensorDevicesAsync();
}
