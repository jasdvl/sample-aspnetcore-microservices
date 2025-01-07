using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Components.Models;
using HomeAnalytica.Web.Components.Validation;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorData : ComponentBase
{
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    [Inject]
    private ISensorDataCollectionService SensorDataService { get; set; } = default!;

    [SupplyParameterFromForm]
    private SensorDataModel TemperatureSensorDataModel { get; set; } = default!;

    [SupplyParameterFromForm]
    private SensorDataModel HumiditySensorDataModel { get; set; } = default!;

    [SupplyParameterFromForm]
    private SensorDataModel EnergyConsumptionSensorDataModel { get; set; } = default!;

    private EditContext? _editContext;

    private List<SensorDeviceDto> _temperatureSensorDevices { get; set; } = new();

    private List<SensorDeviceDto> _humiditySensorDevices { get; set; } = new();

    private List<SensorDeviceDto> _energyConsumptionSensorDevices { get; set; } = new();

    private string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        ErrorMessage = string.Empty;

        InitModels();
        _editContext = new EditContext(TemperatureSensorDataModel);

        // Disable validation-related CSS styles by using a custom FieldCssClassProvider.
        _editContext.SetFieldCssClassProvider(new NoValidationStyleFieldCssProvider());

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
            await SubmitSensorData(TemperatureSensorDataModel);
            await SubmitSensorData(HumiditySensorDataModel);
            await SubmitSensorData(EnergyConsumptionSensorDataModel);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        // Reset models.
        InitModels();
    }

    private void InitModels()
    {
        TemperatureSensorDataModel = new SensorDataModel { MeasuredQuantityId = (int) MeasuredQuantity.Temperature, Timestamp = DateTime.Now };
        HumiditySensorDataModel = new SensorDataModel { MeasuredQuantityId = (int) MeasuredQuantity.Humidity, Timestamp = DateTime.Now };
        EnergyConsumptionSensorDataModel = new SensorDataModel { MeasuredQuantityId = (int) MeasuredQuantity.EnergyConsumption, Timestamp = DateTime.Now };
    }

    private async Task SubmitSensorData(SensorDataModel sensorDataModel)
    {
        if (sensorDataModel.DeviceId != null && sensorDataModel.Value != null)
        {
            var data = new SensorDataDto
            {
                DeviceId = (long) sensorDataModel.DeviceId,
                MeasuredQuantityId = sensorDataModel.MeasuredQuantityId,
                Timestamp = sensorDataModel.Timestamp.ToUniversalTime(),
                Value = (double) sensorDataModel.Value
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
