using HomeAnalytica.DataCollection.DataProcessing;
using HomeAnalytica.Grpc.Contracts.DataCollection;

namespace HomeAnalytica.DataCollection.Factories;

public interface ISensorDataHandlerFactory
{
    ISensorDataProcessor GetDataProcessor(MeasuredQuantity sensorType);
}

public class SensorDataProcessorFactory : ISensorDataHandlerFactory
{
    private readonly Dictionary<MeasuredQuantity, ISensorDataProcessor> _sensorDataHandlers;

    public SensorDataProcessorFactory(
                                ITemperatureDataProcessor temperatureDataProcessor,
                                IEnergyConsumptionDataProcessor energyConsumptionDataProcessor,
                                IHumidityDataProcessor humidityDataProcessor)
    {
        _sensorDataHandlers = new Dictionary<MeasuredQuantity, ISensorDataProcessor>
        {
            { MeasuredQuantity.Temperature, temperatureDataProcessor },
            { MeasuredQuantity.EnergyConsumption, energyConsumptionDataProcessor },
            { MeasuredQuantity.Humidity, humidityDataProcessor }
        };
    }

    public ISensorDataProcessor GetDataProcessor(MeasuredQuantity measuredQuantity)
    {
        if (_sensorDataHandlers.TryGetValue(measuredQuantity, out var sensorDataHandler))
        {
            return sensorDataHandler;
        }

        throw new ArgumentException($"Unsupported sensor type: {measuredQuantity}");
    }
}
