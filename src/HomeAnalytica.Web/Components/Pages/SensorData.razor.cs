using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorData : ComponentBase
{
    private double? _temperature;

    private double? _humidity;

    private double? _energyConsumption;

    private bool isDataSubmitted = false;

    private long? _selectedTemperatureSensorDeviceId;

    private long? _selectedHumiditySensorDeviceId;

    private long? _selectedEnergyConsumptionSensorDeviceId;

    private List<SensorDeviceDto> _temperatureSensorDevices { get; set; } = new();

    private List<SensorDeviceDto> _humiditySensorDevices { get; set; } = new();

    private List<SensorDeviceDto> _energyConsumptionSensorDevices { get; set; } = new();

    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    [Inject]
    private ISensorDataService SensorDataService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadSensorDevicesAsync();
    }

    private async Task SubmitAllSensorData()
    {
        await SubmitSensorData(SensorType.Temperature, _selectedTemperatureSensorDeviceId, _temperature);
        await SubmitSensorData(SensorType.Humidity, _selectedHumiditySensorDeviceId, _humidity);
        await SubmitSensorData(SensorType.EnergyConsumption, _selectedEnergyConsumptionSensorDeviceId, _energyConsumption);

        _selectedTemperatureSensorDeviceId = null;
        _selectedHumiditySensorDeviceId = null;
        _selectedEnergyConsumptionSensorDeviceId = null;

        _temperature = null;
        _humidity = null;
        _energyConsumption = null;
    }

    private async Task SubmitSensorData(SensorType sensorType, long? deviceId, double? value)
    {
        if (deviceId != null && value != null)
        {
            var data = new SensorDataDto
            {
                DeviceId = (long) deviceId,
                SensorType = sensorType,
                Timestamp = DateTime.UtcNow,
                Value = (double) value
            };

            await SensorDataService.ProcessSensorDataAsync(data);
        }
    }

    private async Task LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();

        _temperatureSensorDevices = devices.FindAll(d => d.Type == SensorType.Temperature);
        _humiditySensorDevices = devices.FindAll(d => d.Type == SensorType.Humidity);
        _energyConsumptionSensorDevices = devices.FindAll(d => d.Type == SensorType.EnergyConsumption);
    }
}
