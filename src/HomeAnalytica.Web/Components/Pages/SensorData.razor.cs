using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorData : ComponentBase
{
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    [Inject]
    private ISensorDataCollectionService SensorDataService { get; set; } = default!;

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

    private string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadSensorDevicesAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task SubmitAllSensorData()
    {
        try
        {
            await SubmitSensorData(MeasuredQuantity.Temperature, _selectedTemperatureSensorDeviceId, _temperature);
            await SubmitSensorData(MeasuredQuantity.Humidity, _selectedHumiditySensorDeviceId, _humidity);
            await SubmitSensorData(MeasuredQuantity.EnergyConsumption, _selectedEnergyConsumptionSensorDeviceId, _energyConsumption);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        _selectedTemperatureSensorDeviceId = null;
        _selectedHumiditySensorDeviceId = null;
        _selectedEnergyConsumptionSensorDeviceId = null;

        _temperature = null;
        _humidity = null;
        _energyConsumption = null;
    }

    private async Task SubmitSensorData(MeasuredQuantity measuredQuantity, long? deviceId, double? value)
    {
        if (deviceId != null && value != null)
        {
            var data = new SensorDataDto
            {
                DeviceId = (long) deviceId,
                MeasuredQuantity = measuredQuantity,
                Timestamp = DateTime.UtcNow,
                Value = (double) value
            };

            await SensorDataService.ProcessSensorDataAsync(data);
        }
    }

    private async Task LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();
        _temperatureSensorDevices = devices.FindAll(d => d.MeasuredQuantityId == (int) MeasuredQuantity.Temperature);
        _humiditySensorDevices = devices.FindAll(d => d.MeasuredQuantityId == (int) MeasuredQuantity.Humidity);
        _energyConsumptionSensorDevices = devices.FindAll(d => d.MeasuredQuantityId == (int) MeasuredQuantity.EnergyConsumption);
    }
}
