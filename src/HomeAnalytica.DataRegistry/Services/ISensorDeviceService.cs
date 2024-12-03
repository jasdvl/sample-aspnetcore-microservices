namespace HomeAnalytica.DataRegistry.Services;

using HomeAnalytica.Common.DTOs;

public interface ISensorDeviceService
{
    Task ProcessSensorMetadataAsync(SensorMetadataDto metadata);

    Task<IEnumerable<SensorMetadataDto>> GetAllSensorDevicesAsync();
}
