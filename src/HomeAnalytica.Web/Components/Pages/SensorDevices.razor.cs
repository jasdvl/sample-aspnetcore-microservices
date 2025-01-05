using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Components.Models;
using HomeAnalytica.Web.Components.Validation;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorDevices : ComponentBase
{
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    [Inject]
    private IReferenceDataService ReferenceDataService { get; set; } = default!;

    [SupplyParameterFromForm]
    private SensorDeviceModel? SensorDeviceModel { get; set; }

    private EditContext? _editContext;

    private ReferenceDataDto? _referenceData { get; set; }

    private List<SensorDeviceDto>? _sensorDevices { get; set; }

    private string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ErrorMessage = string.Empty;
        _referenceData = new();
        SensorDeviceModel = new SensorDeviceModel();
        _editContext = new EditContext(SensorDeviceModel);

        // Disable validation-related CSS styles by using a custom FieldCssClassProvider.
        _editContext.SetFieldCssClassProvider(new NoValidationStyleFieldCssProvider());

        try
        {
            _referenceData = await LoadReferenceDataAsync();
            _sensorDevices = await LoadSensorDevicesAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task SubmitSensorDeviceDataAndRefreshDeviceList()
    {
        await SubmitSensorDeviceDataAsync();
        _sensorDevices = await LoadSensorDevicesAsync();
    }

    private async Task SubmitSensorDeviceDataAsync()
    {
        ErrorMessage = string.Empty;

        var data = new SensorDeviceDto
        {
            SerialNo = SensorDeviceModel!.SerialNo,
            Name = SensorDeviceModel.Name,
            MeasuredQuantityId = (int) SensorDeviceModel.MeasuredQuantityId!,
            PhysicalUnitId = (int) SensorDeviceModel.PhysicalUnitId!,
            InstallationDate = SensorDeviceModel.InstallationDate != null
                                                            ? DateTime.SpecifyKind(SensorDeviceModel.InstallationDate.Value, DateTimeKind.Utc)
                                                            : null,
            LastMaintenance = SensorDeviceModel.LastMaintenance != null
                                                            ? DateTime.SpecifyKind(SensorDeviceModel.LastMaintenance.Value, DateTimeKind.Utc)
                                                            : null,
            Status = "Active",
            Description = SensorDeviceModel.Description,
            Location = SensorDeviceModel.Location
        };

        try
        {
            await SensorDeviceService!.PostSensorDeviceAsync(data);
            SensorDeviceModel = new SensorDeviceModel();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task<ReferenceDataDto> LoadReferenceDataAsync()
    {
        var referenceData = await ReferenceDataService.GetReferenceDataAsync();
        return referenceData;
    }

    private async Task<List<SensorDeviceDto>> LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();
        return devices;
    }
}
