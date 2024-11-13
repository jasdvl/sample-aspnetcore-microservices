namespace HomeAnalytica.DataCollection.Services;

using HomeAnalytica.Common.DTOs;

public interface ISensorDataService
{
    Task ProcessSensorDataAsync(SensorDataDto data);
}
