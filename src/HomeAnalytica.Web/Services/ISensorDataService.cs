using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services;

public interface ISensorDataService
{
    Task<List<SensorDataDto>> GetSensorDataAsync(SensorType sensorType, long deviceId);

    Task ProcessSensorDataAsync(SensorDataDto data);
}
