using HomeAnalytica.Common.Const;
using HomeAnalytica.DataCollection.DataProcessing;

namespace HomeAnalytica.DataCollection.Factories;

public interface ISensorDataHandlerFactory
{
    ISensorDataProcessor GetDataProcessor(MeasuredQuantity measuredQuantity);
}

public class SensorDataProcessorFactory : ISensorDataHandlerFactory
{
    private readonly List<ISensorDataProcessor> _sensorDataProcessors;

    public SensorDataProcessorFactory(IEnumerable<ISensorDataProcessor> sensorDataProcessors)
    {
        _sensorDataProcessors = new List<ISensorDataProcessor>(sensorDataProcessors);
    }

    public ISensorDataProcessor GetDataProcessor(MeasuredQuantity measuredQuantity)
    {
        ISensorDataProcessor? sensorDataProcessor = _sensorDataProcessors.FirstOrDefault(p => p.MeasuredQuantity == measuredQuantity);

        if (sensorDataProcessor == null)
        {
            throw new ArgumentException($"Unsupported sensor type: {measuredQuantity}");
        }

        return sensorDataProcessor;
    }
}
