using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataRegistry.Data.Entities;
using HomeAnalytica.DataRegistry.Data.Infrastructure;
using HomeAnalytica.Grpc.Contracts.Protos;

namespace HomeAnalytica.DataRegistry.Services;

public class SensorDeviceService : ISensorDeviceService
{
    private readonly ILogger<SensorDeviceService> _logger;

    private readonly SensorDataSender.SensorDataSenderClient _sensorDataSenderClient;

    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDeviceService"/> class.
    /// </summary>
    /// <param name="logger">The logger for this service.</param>
    /// <param name="sender">The gRPC client for sending sensor data.</param>
    /// <param name="unitOfWork">The unit of work for database operations.</param>
    public SensorDeviceService(
                ILogger<SensorDeviceService> logger,
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

    public async Task<IEnumerable<SensorMetadataDto>> GetAllSensorDevicesAsync()
    {
        var sensorMetadataEntities = await _unitOfWork.SensorMetadataRepository.GetAsync();

        var sensorMetadataDtos = sensorMetadataEntities.Select(entity => new SensorMetadataDto
        {
            DeviceId = entity.DeviceId,
            Type = (Common.Const.SensorType)entity.Type,
            Name = entity.Name,
            InstallationDate = entity.InstallationDate,
            LastMaintenance = entity.LastMaintenance,
            Status = entity.Status,
            Description = entity.Description,
            Location = entity.Location
        });

        return sensorMetadataDtos;
    }

    private async Task AddSensorMetadataAsync(SensorMetadataDto metadata)
    {
        SensorDevice sensorMetadata = new SensorDevice
        {
            DeviceId = metadata.DeviceId,
            Type = (int)metadata.Type,
            Name = metadata.Name,
            InstallationDate = metadata.InstallationDate,
            LastMaintenance = metadata.LastMaintenance,
            Status = metadata.Status,
            Description = metadata.Description,
            Location = metadata.Location
        };

        await _unitOfWork.SensorMetadataRepository.InsertAsync(sensorMetadata);
        await _unitOfWork.SaveAsync();
    }
}
