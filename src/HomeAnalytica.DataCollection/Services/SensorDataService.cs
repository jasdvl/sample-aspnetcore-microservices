using Google.Protobuf.WellKnownTypes;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Infrastructure;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataCollection.Services;

public class SensorDataService : ISensorDataService
{
    private readonly ILogger<SensorDataService> _logger;

    private readonly SensorDataSender.SensorDataSenderClient _sensorDataSenderClient;

    private readonly IUnitOfWork _unitOfWork;

    public SensorDataService(
                ILogger<SensorDataService> logger,
                SensorDataSender.SensorDataSenderClient sender,
                IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _sensorDataSenderClient = sender;
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessSensorDataAsync(SensorDataDto data)
    {
        // Logik zur Verarbeitung der empfangenen Daten
        _logger.LogInformation($"Processing data: Temp={data.Temperature}, Humidity={data.Humidity}, Energy={data.EnergyConsumption}");

        try
        {
            await AddSensorDataAsync(data);

            var res = await _sensorDataSenderClient.SubmitSensorDataAsync(
                                                        new SensorDataRequest()
                                                        {
                                                            Timestamp = Timestamp.FromDateTime(data.Timestamp),
                                                            EnergyConsumption = data.EnergyConsumption,
                                                            Humidity = data.Humidity,
                                                            Temperature = data.Temperature
                                                        });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing sensor data.");

            throw;
        }
    }

    public async Task AddSensorDataAsync(SensorDataDto data)
    {
        SensorData sensorData = new SensorData
        {
            Timestamp = data.Timestamp,
            EnergyConsumption = data.EnergyConsumption,
            Humidity = data.Humidity,
            Temperature = data.Temperature
        };

        await _unitOfWork.SensorDataRepository.InsertAsync(sensorData);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<SensorDataDto>> GetAllSensorDataAsync()
    {
        var sensorDataEntities = await _unitOfWork.SensorDataRepository.GetAsync();

        var sensorDataDtos = sensorDataEntities.Select(entity => new SensorDataDto
        {
            Timestamp = entity.Timestamp,
            Temperature = entity.Temperature,
            Humidity = entity.Humidity,
            EnergyConsumption = entity.EnergyConsumption
        });

        return sensorDataDtos;
    }
}
