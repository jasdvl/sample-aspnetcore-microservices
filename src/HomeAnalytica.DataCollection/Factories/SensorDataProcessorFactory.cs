using HomeAnalytica.DataCollection.DataProcessing;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.Factories;

public interface ISensorDataHandlerFactory
{
    ISensorDataProcessor GetDataProcessor(SensorType sensorType);
}

public class SensorDataProcessorFactory : ISensorDataHandlerFactory
{
    private readonly Dictionary<SensorType, ISensorDataProcessor> _sensorDataHandlers;

    public SensorDataProcessorFactory(
                                ITemperatureDataProcessor temperatureDataProcessor,
                                IEnergyConsumptionDataProcessor energyConsumptionDataProcessor,
                                IHumidityDataProcessor humidityDataProcessor)
    {
        _sensorDataHandlers = new Dictionary<SensorType, ISensorDataProcessor>
        {
            { SensorType.Temperature, temperatureDataProcessor },
            { SensorType.EnergyConsumption, energyConsumptionDataProcessor },
            { SensorType.Humidity, humidityDataProcessor }
        };
    }

    public ISensorDataProcessor GetDataProcessor(SensorType sensorType)
    {
        if (_sensorDataHandlers.TryGetValue(sensorType, out var sensorDataHandler))
        {
            return sensorDataHandler;
        }

        throw new ArgumentException($"Unsupported sensor type: {sensorType}");
    }
}
