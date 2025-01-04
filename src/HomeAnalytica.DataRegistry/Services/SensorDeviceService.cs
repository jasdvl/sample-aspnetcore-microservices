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
    /// <param name="unitOfWork">The unit of work for database operations.</param>
    public SensorDeviceService(
                ILogger<SensorDeviceService> logger,
                IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task AddSensorDeviceAsync(SensorDeviceDto deviceDto)
    {
        _logger.LogInformation($"Processing sensor metadata: Value = {deviceDto.Name}");

        try
        {
            await InsertSensorDeviceAsync(deviceDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing sensor data.");

            throw;
        }
    }

    public async Task<IEnumerable<SensorDeviceDto>> GetAllSensorDevicesAsync()
    {
        var sensorDeviceEntities = await _unitOfWork.SensorDeviceRepository.GetAsync(null, null, x => x.MeasuredQuantity, y => y.PhysUnit);

        var sensorDeviceDtos = sensorDeviceEntities.Select(entity => new SensorDeviceDto
        {
            Id = entity.Id,
            SerialNo = entity.SerialNo,
            MeasuredQuantityId = entity.MeasuredQuantityId,
            MeasuredQuantity = new MeasuredQuantityDto { Id = entity.MeasuredQuantity.Id, Name = entity.MeasuredQuantity.Name, Description = entity.MeasuredQuantity.Description },
            PhysicalUnitId = entity.PhysUnitId,
            PhysicalUnit = new PhysicalUnitDto { Id = entity.PhysUnit.Id, Symbol = entity.PhysUnit.Symbol, Name = entity.PhysUnit.Name, Description = entity.PhysUnit.Description },
            Name = entity.Name,
            InstallationDate = entity.InstallationDate,
            LastMaintenance = entity.LastMaintenance,
            Status = entity.Status,
            Description = entity.Description,
            Location = entity.Location
        });

        return sensorDeviceDtos;
    }

    private async Task InsertSensorDeviceAsync(SensorDeviceDto deviceDto)
    {
        SensorDevice sensorDevice = new SensorDevice
        {
            SerialNo = deviceDto.SerialNo,
            MeasuredQuantityId = deviceDto.MeasuredQuantityId,
            PhysUnitId = deviceDto.PhysicalUnitId,
            Name = deviceDto.Name,
            InstallationDate = deviceDto.InstallationDate,
            LastMaintenance = deviceDto.LastMaintenance,
            Status = deviceDto.Status,
            Description = deviceDto.Description,
            Location = deviceDto.Location
        };

        await _unitOfWork.SensorDeviceRepository.InsertAsync(sensorDevice);
        await _unitOfWork.SaveAsync();
    }
}
