using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataRegistry.Data.Entities;
using HomeAnalytica.DataRegistry.Data.Infrastructure;

namespace HomeAnalytica.DataRegistry.Services;

public class SensorDeviceService : ISensorDeviceService
{
    private readonly ILogger<SensorDeviceService> _logger;

    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDeviceService"/> class.
    /// </summary>
    /// <param name="logger">The logger for this service.</param>
    /// <param name="sender">The gRPC client for sending sensor data.</param>
    /// <param name="unitOfWork">The unit of work for database operations.</param>
    public SensorDeviceService(
                ILogger<SensorDeviceService> logger,
                IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessSensorMetadataAsync(SensorDeviceDto metadata)
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

    public async Task<IEnumerable<SensorDeviceDto>> GetAllSensorDevicesAsync()
    {
        var sensorDeviceEntities = await _unitOfWork.SensorDeviceRepository.GetAsync();

        var sensorDeviceDtos = sensorDeviceEntities.Select(entity => new SensorDeviceDto
        {
            Id = entity.Id,
            SerialNo = entity.SerialNo,
            MeasuredQuantity = (Common.Const.MeasuredQuantity) entity.MeasuredQuantityId,
            PhysUnit = (Common.Const.PhysicalUnit) entity.PhysUnitId,
            Name = entity.Name,
            InstallationDate = entity.InstallationDate,
            LastMaintenance = entity.LastMaintenance,
            Status = entity.Status,
            Description = entity.Description,
            Location = entity.Location
        });

        return sensorDeviceDtos;
    }

    private async Task AddSensorMetadataAsync(SensorDeviceDto metadata)
    {
        SensorDevice sensorDevice = new SensorDevice
        {
            SerialNo = metadata.SerialNo,
            MeasuredQuantityId = (int) metadata.MeasuredQuantity,
            PhysUnitId = (int) metadata.PhysUnit,
            Name = metadata.Name,
            InstallationDate = metadata.InstallationDate,
            LastMaintenance = metadata.LastMaintenance,
            Status = metadata.Status,
            Description = metadata.Description,
            Location = metadata.Location
        };

        await _unitOfWork.SensorDeviceRepository.InsertAsync(sensorDevice);
        await _unitOfWork.SaveAsync();
    }
}
