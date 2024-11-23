using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataRegistry.Data.Entities;
using HomeAnalytica.DataRegistry.Data.Infrastructure;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataRegistry.Services;

public class SensorMetadataService : ISensorMetadataService
{
    private readonly ILogger<SensorMetadataService> _logger;

    private readonly SensorDataSender.SensorDataSenderClient _sensorDataSenderClient;

    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorMetadataService"/> class.
    /// </summary>
    /// <param name="logger">The logger for this service.</param>
    /// <param name="sender">The gRPC client for sending sensor data.</param>
    /// <param name="unitOfWork">The unit of work for database operations.</param>
    public SensorMetadataService(
                ILogger<SensorMetadataService> logger,
                SensorDataSender.SensorDataSenderClient sender,
                IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _sensorDataSenderClient = sender;
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessSensorMetadataAsync(SensorMetadataDto metadata)
    {
        _logger.LogInformation($"Processing sensor metadata: Value = {metadata.Name}");

        try
        {
            await AddSensorMetadataAsync(metadata);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing sensor data.");

            throw;
        }
    }

    private async Task AddSensorMetadataAsync(SensorMetadataDto metadata)
    {
        SensorMetadata sensorMetadata = new SensorMetadata
        {
            Name = metadata.Name,
            LastMaintenance = metadata.LastMaintenance
        };

        await _unitOfWork.SensorMetadataRepository.InsertAsync(sensorMetadata);
        await _unitOfWork.SaveAsync();
    }

    private async Task<IEnumerable<SensorMetadataDto>> GetAllSensorMetadataAsync()
    {
        var sensorMetadataEntities = await _unitOfWork.SensorMetadataRepository.GetAsync();

        var sensorMetadataDtos = sensorMetadataEntities.Select(entity => new SensorMetadataDto
        {
            Name = entity.Name
        });

        return sensorMetadataDtos;
    }
}
