namespace HomeAnalytica.DataCollection.Services;

using global::HomeAnalytica.DataCollection.DTOs;

public interface ISensorDataService
{
    Task ProcessSensorDataAsync(SensorDataDto data);
}
