using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services;

public interface ISensorDataService
{
    Task ProcessSensorDataAsync(SensorDataDto data);
}
