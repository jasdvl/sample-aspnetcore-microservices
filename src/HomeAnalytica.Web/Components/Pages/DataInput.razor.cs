using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class DataInput : ComponentBase
{
    private double _temperature;

    private double _humidity;

    private double _energyConsumption;

    private bool isDataSubmitted = false;

    private string? _selectedTempSensorId;

    private string? _selectedHumiditySensorId;

    private string? _selectedEnergyConsumptionSensorId;

    private List<SensorMetadataDto> _tempSensorDevices { get; set; } = new();

    private List<SensorMetadataDto> _humiditySensorDevices { get; set; } = new();

    private List<SensorMetadataDto> _energyConsumptionSensorDevices { get; set; } = new();

    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadSensorDevicesAsync();
    }

    private async Task SubmitData()
    {
        var res = await SensorDataService.GetSensorDataAsync(SensorType.Temperature);

        if (!string.IsNullOrEmpty(_selectedTempSensorId))
        {
            var data = new SensorDataDto
            {
                DeviceId = _selectedTempSensorId,
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
