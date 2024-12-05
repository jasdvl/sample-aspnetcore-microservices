using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorData : ComponentBase
{
    private double _temperature;

    private double _humidity;

    private double _energyConsumption;

    private bool isDataSubmitted = false;

    private long? _selectedTempSensorId;

    private long? _selectedHumiditySensorId;

    private long? _selectedEnergyConsumptionSensorId;

    private List<SensorDeviceDto> _tempSensorDevices { get; set; } = new();

    private List<SensorDeviceDto> _humiditySensorDevices { get; set; } = new();

    private List<SensorDeviceDto> _energyConsumptionSensorDevices { get; set; } = new();

    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadSensorDevicesAsync();
    }

    private async Task SubmitData()
    {
        if (_selectedTempSensorId != null)
        {
            var data = new SensorDataDto
            {
                DeviceId = (long)_selectedTempSensorId,
                SensorType = SensorType.Temperature,
                Timestamp = DateTime.UtcNow,
                Value = _temperature
            };

            await SensorDataService.ProcessSensorDataAsync(data);

            _selectedTempSensorId = null;
            _temperature = 0;
        }

    }

    private async Task LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();

        _tempSensorDevices = devices.FindAll(d => d.Type == SensorType.Temperature);
        _humiditySensorDevices = devices.FindAll(d => d.Type == SensorType.Humidity);
        _energyConsumptionSensorDevices = devices.FindAll(d => d.Type == SensorType.EnergyConsumption);
    }
}
