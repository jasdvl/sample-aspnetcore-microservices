using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services;

public interface ISensorDataService
{
    Task<List<SensorDataDto>> GetSensorDataAsync(HomeAnalytica.Common.Const.SensorType sensorType);

    Task ProcessSensorDataAsync(SensorDataDto data);
}
