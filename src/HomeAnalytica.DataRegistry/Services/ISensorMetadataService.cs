namespace HomeAnalytica.DataRegistry.Services;

using HomeAnalytica.Common.DTOs;

public interface ISensorMetadataService
{
    Task ProcessSensorMetadataAsync(SensorMetadataDto metadata);
}
