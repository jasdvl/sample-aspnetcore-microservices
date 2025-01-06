using HomeAnalytica.Common.Const;
using HomeAnalytica.DataCollection.DataProcessing;

namespace HomeAnalytica.DataCollection.Factories;

public interface ISensorDataHandlerFactory
{
    ISensorDataProcessor GetDataProcessor(MeasuredQuantity measuredQuantity);
}

public class SensorDataProcessorFactory : ISensorDataHandlerFactory
{
    private readonly List<ISensorDataProcessor> _sensorDataHandlers;

    public SensorDataProcessorFactory(
                                ITemperatureDataProcessor temperatureDataProcessor,
                                IEnergyConsumptionDataProcessor energyConsumptionDataProcessor,
                                IHumidityDataProcessor humidityDataProcessor)
    {
        _sensorDataHandlers = new List<ISensorDataProcessor>
        {
            { temperatureDataProcessor },
            { energyConsumptionDataProcessor },
            { humidityDataProcessor }
        };
    }

    public ISensorDataProcessor GetDataProcessor(MeasuredQuantity measuredQuantity)
    {
        ISensorDataProcessor? sensorDataProcessor = _sensorDataHandlers.FirstOrDefault(p => p.MeasuredQuantity == measuredQuantity);

        if (sensorDataProcessor == null)
        {
            throw new ArgumentException($"Unsupported sensor type: {measuredQuantity}");
        }

        return sensorDataProcessor;
    }
}
