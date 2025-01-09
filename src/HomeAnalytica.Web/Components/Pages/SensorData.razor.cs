using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Components.Models;
using HomeAnalytica.Web.Components.Validation;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HomeAnalytica.Web.Components.Pages;

/// <summary>
/// This class represents a component for displaying and submitting sensor data (e.g., temperature, humidity, energy consumption).
/// </summary>
public partial class SensorData : ComponentBase
{
    /// <summary>
    /// Service to interact with sensor devices.
    /// </summary>
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    /// <summary>
    /// Service for collecting and processing sensor data.
    /// </summary>
    [Inject]
    private ISensorDataCollectionService SensorDataService { get; set; } = default!;

    /// <summary>
    /// Service to show toast notifications to the user.
    /// </summary>
    [Inject]
    public ToastNotificationService ToastNotificationService { get; set; } = default!;


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

    /// <summary>
    /// Called when the component is initialized. It loads sensor devices and prepares models.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
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

    /// <summary>
    /// Submits all sensor data and shows a success message if any data was saved.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task SubmitAllSensorData()
    {
        try
        {
            var tempSaved = await SubmitSensorData(TemperatureSensorDataModel);
            var humiditySaved = await SubmitSensorData(HumiditySensorDataModel);
            var energySaved = await SubmitSensorData(EnergyConsumptionSensorDataModel);

            if (tempSaved || humiditySaved || energySaved)
            {
                var message = BuildSaveSuccessMessage();
                DisplaySaveSuccessMessage(message);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

        // Reset models after submission
        InitModels();
    }

    /// <summary>
    /// Builds the success message containing details of all saved sensor data.
    /// </summary>
    /// <returns>A string containing the success message.</returns>
    private string BuildSaveSuccessMessage()
    {
        var message = string.Empty;
        var device = _temperatureSensorDevices.FirstOrDefault(d => d.Id == TemperatureSensorDataModel.DeviceId);
        message += BuildSaveSuccessMessageLine(device);
        device = _humiditySensorDevices.FirstOrDefault(d => d.Id == HumiditySensorDataModel.DeviceId);
        message += BuildSaveSuccessMessageLine(device);
        device = _energyConsumptionSensorDevices.FirstOrDefault(d => d.Id == EnergyConsumptionSensorDataModel.DeviceId);
        message += BuildSaveSuccessMessageLine(device);

        return message;
    }

    private string BuildSaveSuccessMessageLine(SensorDeviceDto? device)
    {
        if (device == null)
        {
            return string.Empty;
        }

        var measuredQuantity = device.MeasuredQuantity.Name;
        var physUnit = device.PhysicalUnit.Symbol;
        var message = $"{measuredQuantity}: {TemperatureSensorDataModel.Timestamp.ToString("yy-MM-dd hh:mm tt")} {TemperatureSensorDataModel.Value} {physUnit}\r\n";
        return message;
    }

    private void InitModels()
    {
        TemperatureSensorDataModel = new SensorDataModel { MeasuredQuantityId = (int) MeasuredQuantity.Temperature, Timestamp = DateTime.Now };
        HumiditySensorDataModel = new SensorDataModel { MeasuredQuantityId = (int) MeasuredQuantity.Humidity, Timestamp = DateTime.Now };
        EnergyConsumptionSensorDataModel = new SensorDataModel { MeasuredQuantityId = (int) MeasuredQuantity.EnergyConsumption, Timestamp = DateTime.Now };
    }

    /// <summary>
    /// Submits sensor data for a single sensor model.
    /// </summary>
    /// <param name="sensorDataModel">The sensor data model containing data to be submitted.</param>
    /// <returns>A task that represents the asynchronous operation. Returns true if the data was successfully submitted.</returns>
    private async Task<bool> SubmitSensorData(SensorDataModel sensorDataModel)
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
            return true;
        }

        return false;
    }

    private async Task LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();
        _temperatureSensorDevices = devices.FindAll(d => d.MeasuredQuantityId == (int) MeasuredQuantity.Temperature);
        _humiditySensorDevices = devices.FindAll(d => d.MeasuredQuantityId == (int) MeasuredQuantity.Humidity);
        _energyConsumptionSensorDevices = devices.FindAll(d => d.MeasuredQuantityId == (int) MeasuredQuantity.EnergyConsumption);
    }

    private void DisplaySaveSuccessMessage(string message)
    {
        ToastNotificationService.ShowToast(message, "Success", ToastNotificationType.Success);
    }
}
